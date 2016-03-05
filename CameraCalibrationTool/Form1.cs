using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using OpenCvSharp;
using OpenCvSharp.Extensions;


//ref http://opencv.jp/sample/camera_calibration.html
//ref http://opencvsharp.googlecode.com/svn/trunk/2.4.5/OpenCvSharp.Test/Samples/CalibrateCamera.cs
//ref http://nullege.com/codes/search/cv.InitUndistortMap

namespace CameraCalibrationTool
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////
		//// User Define
		//// I Use Data is http://opencv.jp/sample/pics/chesspattern_7x10.pdf
		/////////////////////////////////////////////////////////////
		int _PatRow = 7;
		int _PatCol = 10;
		float _ChessSize = 24.0f; //milimeter
		/////////////////////////////////////////////////////////////

		IplImage _mapX, _mapY;
		CvMat _fileIntrinsic, _fileDistortion;
		bool _remapTimerStart = false;
		bool _captureTimerStart = false;

		string _parentDirPath = "";
		string _saveDirPath = "";

		bool _captureFlag = false;
		int _nowCaptureIndex = 0;

		string[] _readDetectFiles, _calibrateFiles;

		private void Form1_Load(object sender, EventArgs e)
		{
			
			//make calibration data folder in desktop
			_parentDirPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\yt_CCT";
			if (!Directory.Exists(_parentDirPath)) Directory.CreateDirectory(_parentDirPath);

			//Capture Prepare
			SetCaptureWebCam();
			captureTimer.Start();

			//compare timer prepare
			SetCompareWebCam();
		}

		/////////////////////////////////////////////////////////////
		//// Capture Set up
		/////////////////////////////////////////////////////////////
		private void SetCaptureWebCam()
		{
			try
			{
				var cap = Cv.CreateCameraCapture(0);
				var Ipl = new IplImage();

				captureTimer.Tick += (sss, eee) =>
				{
					Ipl = Cv.QueryFrame(cap);

					if (!_captureTimerStart)
					{
						ResizeUI(Ipl.Width, Ipl.Height, captureBox);
						_captureTimerStart = true;
					}

					if (captureBox.Image != null) captureBox.Image.Dispose();
					captureBox.Image = Ipl.ToBitmap();
					if (_captureFlag)
					{
						if (_nowCaptureIndex == 0)
						{
							_saveDirPath = _parentDirPath + @"\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
							if (!Directory.Exists(_saveDirPath)) Directory.CreateDirectory(_saveDirPath);
						}
						CaptureButton.Enabled = false;
						Ipl.SaveImage(_saveDirPath + @"\" + _nowCaptureIndex.ToString("000") + ".jpg");
						_captureFlag = false;
						_nowCaptureIndex++;
						CaptureButton.Enabled = true;
					}
					Ipl.Dispose();
				};

				captureTimer.Interval = 50;

			}
			catch { return; }

		}

		private void ResizeUI(int imageWidth, int imageHeight, PictureBox pic)
		{
			pic.Height = imageHeight;
			pic.Width = imageWidth;

			if (this.Height < imageHeight)
			{
				this.Height = imageHeight + 150;
				this.Width = imageWidth + 150;
				this.tabControl1.Width = imageWidth + 120;
				this.tabControl1.Height = imageHeight + 120;
			}
		}

		private void CaptureButton_Click(object sender, EventArgs e)
		{
			_captureFlag = true;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			captureTimer.Stop();
			compareTimer.Stop();
		}

		/////////////////////////////////////////////////////////////
		//// Detect Set up
		/////////////////////////////////////////////////////////////
		private void ReadBoardImage_Click(object sender, EventArgs e)
		{
			//folder dialog 
			var fbd = new FolderBrowserDialog();
			var result = fbd.ShowDialog();
			if (result != DialogResult.OK) return;
			_readDetectFiles = Directory.GetFiles(fbd.SelectedPath);


			//comboBox action
			List<string> checkResult;
			var dst = DrawCheckerImg(_readDetectFiles, out checkResult);

			for (int i = 0; i < _readDetectFiles.Length; i++)
			{
				var f = _readDetectFiles[i];
				if (!f.EndsWith(".jpg"))
				{
					detectComboBox.Items.Clear();
					break;
				}
				detectComboBox.Items.Add(checkResult[i] + Path.GetFileName(f));
			}
			if (_readDetectFiles.Count() > 0) detectComboBox.SelectedIndex = 0;
			else return;


			//first set
			var bmp = new Bitmap(dst[0].ToBitmap());
			ResizeUI(bmp.Width, bmp.Height, detectPictureBox);
			detectPictureBox.Image = bmp;

			//next set
			detectComboBox.SelectedIndexChanged += (ee, ss) =>
			{
				var bitmap = new Bitmap(dst[detectComboBox.SelectedIndex].ToBitmap());
				if (detectPictureBox.Image != null) detectPictureBox.Image.Dispose();
				ResizeUI(bitmap.Width, bitmap.Height, detectPictureBox);
				detectPictureBox.Image = bitmap;
			};
		}

		private List<IplImage> DrawCheckerImg(string[] filePaths, out List<string> connerResults)
		{
			var dst = new List<IplImage>();
			connerResults = new List<string>();

			foreach (var f in filePaths)
			{
				CvPoint2D32f[] conners;
				var connerCount = 0;
				var img = Cv.LoadImage(f);
				var gray = Cv.LoadImage(f, LoadMode.GrayScale);
				var found = Cv.FindChessboardCorners(gray, new CvSize(_PatCol, _PatRow), out conners, out connerCount, ChessboardFlag.AdaptiveThresh);

				if (found)
				{
					Cv.FindCornerSubPix(gray, conners, connerCount, new CvSize(3, 3), new CvSize(-1, -1), new CvTermCriteria(20, 0.03));

					for (int i = 0; i < connerCount; i++)
						Cv.Circle(img, conners[i], 3, new CvColor(255, 0, 0), 2);
					connerResults.Add("Correct_");
				}
				else connerResults.Add("Error_");
				dst.Add(img);
			}

			return dst;
		}


		/////////////////////////////////////////////////////////////
		//// Calibrate 
		/////////////////////////////////////////////////////////////
		private void CalibrateButton_Click(object sender, EventArgs e)
		{
			//folder dialog 
			var fbd = new FolderBrowserDialog();
			var result = fbd.ShowDialog();
			if (result != DialogResult.OK) return;
			_calibrateFiles = Directory.GetFiles(fbd.SelectedPath);
			Calibrate(_calibrateFiles);
		}

		private void Calibrate(string[] imageFilePaths)
		{
			var patSize = _PatRow * _PatCol;

			//set image data (jpg only)
			var imgList = new List<IplImage>();
			foreach (var f in imageFilePaths.Where(x => x.EndsWith(".jpg")))
				imgList.Add(new IplImage(f, LoadMode.Color));

			var imgNum = imgList.Count();
			var allPoints = imgNum * patSize;


			//define 3d area point
			CvPoint3D32f[,,] objects = new CvPoint3D32f[imgNum, _PatRow, _PatCol];
			for (int i = 0; i < imgNum; i++)
			{
				for (int j = 0; j < _PatRow; j++)
				{
					for (int k = 0; k < _PatCol; k++)
					{
						objects[i, j, k] = new CvPoint3D32f
						{
							X = j * _ChessSize,
							Y = k * _ChessSize,
							Z = 0.0f
						};
					}
				}
			}

			var objectPoints = new CvMat(allPoints, 3, MatrixType.F32C1, objects);

			// Get conners data from chess board
			var patternSize = new CvSize(_PatCol, _PatRow);
			var allCorners = new List<CvPoint2D32f>(allPoints);
			var check = true;
			var pointCountsValue = new int[imgNum];
			for (var i = 0; i < imgNum; i++)
			{
				CvPoint2D32f[] corners;
				var found = Cv.FindChessboardCorners(imgList[i], patternSize, out corners);
				if (!found)
				{
					MessageBox.Show("Error corners " + imageFilePaths[i]);
					check = false;
					break;
				}

				//fix conner position
				using (IplImage srcGray = new IplImage(imgList[i].Size, BitDepth.U8, 1))
				{
					Cv.CvtColor(imgList[i], srcGray, ColorConversion.BgrToGray);
					Cv.FindCornerSubPix(srcGray, corners, corners.Length, new CvSize(3, 3), new CvSize(-1, -1), new CvTermCriteria(20, 0.03));
					pointCountsValue[i] = corners.Length;
				}
				allCorners.AddRange(corners);

			}
			if (!check) return;


			var imagePoints = new CvMat(allPoints, 1, MatrixType.F32C2, allCorners.ToArray());
			var pointCounts = new CvMat(imgNum, 1, MatrixType.S32C1, pointCountsValue);

			//Estimation of Internal parameters 
			var intrinsic = new CvMat(3, 3, MatrixType.F64C1);
			var distortion = new CvMat(1, 4, MatrixType.F64C1);
			var rotation = new CvMat(imgNum, 3, MatrixType.F64C1);
			var translation = new CvMat(imgNum, 3, MatrixType.F64C1);

			Cv.CalibrateCamera2(objectPoints, imagePoints, pointCounts, imgList[0].Size, intrinsic, distortion, rotation, translation, CalibrationFlag.Default);


			//Estimation of external parameters  
			CvMat subImagePoints, subObjectPoints;
			Cv.GetRows(imagePoints, out subImagePoints, 0, patSize);
			Cv.GetRows(objectPoints, out subObjectPoints, 0, patSize);
			var rotation_ = new CvMat(1, 3, MatrixType.F32C1);
			var translation_ = new CvMat(1, 3, MatrixType.F32C1);

			Cv.FindExtrinsicCameraParams2(subObjectPoints, subImagePoints, intrinsic, distortion, rotation_, translation_, false);

			var savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "camera.xml";
			//write xml
			using (var fs = new CvFileStorage(savePath, null, FileStorageMode.Write))
			{
				fs.Write("intrinsic", intrinsic);
				fs.Write("rotation", rotation_);
				fs.Write("translation", translation_);
				fs.Write("distortion", distortion);
			}

			//release
			foreach (var img in imgList)
				img.Dispose();

			MessageBox.Show("Success " + savePath);
		}


		/////////////////////////////////////////////////////////////
		//// Result Comapre Normal Image and Remap Image
		/////////////////////////////////////////////////////////////
		private void SetCompareWebCam()
		{
			try
			{
				var cap = Cv.CreateCameraCapture(0);
				var Ipl = new IplImage();
				var dst = new IplImage();

				compareTimer.Tick += (sss, eee) =>
				{
					Ipl = Cv.QueryFrame(cap);
					dst = new IplImage(Ipl.Size, Ipl.Depth, Ipl.NChannels);

					if (_remapTimerStart)
					{
						var imageWidth = Ipl.Width;
						var imageHeight = Ipl.Height;

						ResizeUI(imageWidth, imageHeight, compareRawPic);

						//add one picturebox
						compareRemapPic.Location = new Point(imageWidth + 50, compareRawPic.Location.Y);
						compareRemapPic.Height = imageHeight;
						compareRemapPic.Width = imageWidth;
			

						this.tabControl1.Width = imageWidth * 2 + 120;
						this.tabControl1.Height = imageHeight + 120;
						this.Height = imageHeight + 150;
						this.Width = imageWidth * 2 + 150;

						//set rectify data
						_mapX = Cv.CreateImage(Ipl.Size, BitDepth.F32, 1);
						_mapY = Cv.CreateImage(Ipl.Size, BitDepth.F32, 1);
						Cv.InitUndistortMap(_fileIntrinsic, _fileDistortion, _mapX, _mapY);
						_remapTimerStart = false;
					}

					var ld  = DrawLattice(Ipl, 30, 0);

					//set raw 
					if (compareRawPic.Image != null) compareRawPic.Image.Dispose();
					compareRawPic.Image = ld.ToBitmap();

					//set remap
					if (compareRemapPic.Image != null) compareRemapPic.Image.Dispose();
					Cv.Remap(Ipl, dst, _mapX, _mapY);
					var dd = DrawLattice(dst, 30, 1);
					compareRemapPic.Image =  dd.ToBitmap();

					Ipl.Dispose();
					dst.Dispose();
					ld.Dispose();
					dd.Dispose();
				};

				captureTimer.Interval = 50;

			}
			catch (Exception ex)
			{
				return;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ipl"></param>
		/// <param name="size">lattice size</param>
		/// <param name="lineType">0 = Red 1 = Blue , Other = Green</param>
		/// <returns></returns>
		private IplImage DrawLattice(IplImage ipl, int size, int lineType)
		{
			unsafe
			{
				byte* ptr = (byte*)ipl.ImageData;
				for (int y = 0; y < ipl.Height; y++)
				{
					for (int x = 0; x < ipl.Width; x++)
					{
						if (y % size == 0 || x % size == 0)
						{
							int offset = (ipl.WidthStep * y) + (x * 3);
							if (lineType == 0)
							{
								ptr[offset + 0] = 0; //B
								ptr[offset + 1] = 0;   //G
								ptr[offset + 2] = 255;   //R
							}
							else if (lineType == 1)
							{
								ptr[offset + 0] = 255;
								ptr[offset + 1] = 0;
								ptr[offset + 2] = 0;
							}
							else {
								ptr[offset + 0] = 0;
								ptr[offset + 1] = 255;
								ptr[offset + 2] = 0;
							}
						}
					}
				}
			}
			return ipl;
		}



		private void ReadClibButton_Click(object sender, EventArgs e)
		{
			//captureTimer.Stop();
			//compareTimer.Stop();

			//read file
			var openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "Calib Files (.xml)|*.xml";
			openFileDialog1.FilterIndex = 1;
			openFileDialog1.Multiselect = false;

			var check = openFileDialog1.ShowDialog();

			try
			{
				// Process input if the user clicked OK.
				if (check == DialogResult.OK)
				{
					var fname = openFileDialog1.FileName;
					//MessageBox.Show(fname);
					//make remap data

					//read file
					//Internal parameters 
					
					using (var fs = new CvFileStorage(fname, null, FileStorageMode.Read))
					{
						var param = fs.GetFileNodeByName(null, "intrinsic");
						_fileIntrinsic = fs.Read<CvMat>(param);
						param = fs.GetFileNodeByName(null, "distortion");
						_fileDistortion = fs.Read<CvMat>(param);
					}
					_remapTimerStart = true;
					_captureTimerStart = true;
					//timer start
					compareTimer.Start();
				}
			}
			catch
			{
				MessageBox.Show("File format is Error");
			}
		}
	}
}
