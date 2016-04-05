namespace XLabs.Forms.Controls.MonoDroid.TimesSquare
{
	using System;

	using Android.Content;
	using Android.Runtime;
	using Android.Util;
	using Android.Widget;

	using Android.Graphics; // Paint
	using Xamarin.Forms;  // Device

	/// <summary>
	/// Class CalendarCellView.
	/// </summary>
	public class CalendarCellView : TextView
	{

		/// <summary>
		/// The _is selectable
		/// </summary>
		private bool _isSelectable;
		/// <summary>
		/// The _is current month
		/// </summary>
		private bool _isCurrentMonth;
		/// <summary>
		/// The _is today
		/// </summary>
		private bool _isToday;
		/// <summary>
		/// The _is highlighted
		/// </summary>
		private bool _isHighlighted;
		/// <summary>
		/// The _range state
		/// </summary>
		private RangeState _rangeState = RangeState.None;

		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarCellView"/> class.
		/// </summary>
		/// <param name="handle">The handle.</param>
		/// <param name="transfer">The transfer.</param>
		public CalendarCellView(IntPtr handle, JniHandleOwnership transfer)
			: base(handle, transfer)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarCellView"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public CalendarCellView(Context context)
			: base(context)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarCellView"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="attrs">The attrs.</param>
		public CalendarCellView(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarCellView"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="attrs">The attrs.</param>
		/// <param name="defStyle">The definition style.</param>
		public CalendarCellView(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle)
		{
		}

		/// <summary>
		/// Sets a value indicating whether this <see cref="CalendarCellView"/> is selectable.
		/// </summary>
		/// <value><c>true</c> if selectable; otherwise, <c>false</c>.</value>
		public bool Selectable {
			set {
				_isSelectable = value;
			}
		}

		/// <summary>
		/// Sets a value indicating whether this instance is current month.
		/// </summary>
		/// <value><c>true</c> if this instance is current month; otherwise, <c>false</c>.</value>
		public bool IsCurrentMonth {
			set {
				_isCurrentMonth = value;
			}
		}

		/// <summary>
		/// Sets a value indicating whether this instance is today.
		/// </summary>
		/// <value><c>true</c> if this instance is today; otherwise, <c>false</c>.</value>
		public bool IsToday {
			set {
				_isToday = value;
			}
		}

		/// <summary>
		/// Sets a value indicating whether this instance is highlighted.
		/// </summary>
		/// <value><c>true</c> if this instance is highlighted; otherwise, <c>false</c>.</value>
		public bool IsHighlighted {
			set {
				_isHighlighted = value;
			}
		}

		/// <summary>
		/// Sets the state of the range.
		/// </summary>
		/// <value>The state of the range.</value>
		public RangeState RangeState {
			set {
				_rangeState = value;
			}
		}

		/// <summary>
		/// Sets the style.
		/// </summary>
		/// <param name="style">The style.</param>
		public void SetStyle(StyleDescriptor style)
		{
			if(style.DateLabelFont != null)
			{
				this.Typeface = (style.DateLabelFont);
			}
			if(this.Selected)
			{
				SetBackgroundColor(style.SelectedDateBackgroundColor);
				SetTextColor(style.SelectedDateForegroundColor);
			} else if(_isToday)
			{
				SetBackgroundColor(style.TodayBackgroundColor);
				SetTextColor(style.TodayForegroundColor);
			} else if(_isHighlighted)
			{
				SetBackgroundColor(style.HighlightedDateBackgroundColor);
				if(_isCurrentMonth)
				{
					SetTextColor(style.HighlightedDateForegroundColor);
				} else
				{
					SetTextColor(style.InactiveDateForegroundColor);
				}
			} else if(!_isCurrentMonth)
			{
				SetBackgroundColor(style.InactiveDateBackgroundColor);
				SetTextColor(style.InactiveDateForegroundColor);
			} else
			{
				SetBackgroundColor(style.DateBackgroundColor);
				SetTextColor(style.DateForegroundColor);
			}
		}

		////////////////////

		// from http://stackoverflow.com/questions/12166476/android-canvas-drawtext-set-font-size-from-width
		private static void SetTextSizeForWidth(Paint paint, float desiredWidth,
			String text) {

			// Pick a reasonably large value for the test. Larger values produce
			// more accurate results, but may cause problems with hardware
			// acceleration. But there are workarounds for that, too; refer to
			// http://stackoverflow.com/questions/6253528/font-size-too-large-to-fit-in-cache
			const float testTextSize = 48f;

			// Get the bounds of the text, using our testTextSize.
			paint.TextSize = testTextSize;
			Rect bounds = new Rect();
			paint.GetTextBounds(text, 0, text.Length, bounds);

			// Calculate the desired size as a proportion of our testTextSize.
			float desiredTextSize = testTextSize * desiredWidth / bounds.Width();

			// Set the paint for that size.
			paint.TextSize = desiredTextSize;
		}

        //TC
        private static void SetTextSizeForHeight(Paint paint, float desiredHeight,
            String text) {

            // Pick a reasonably large value for the test. Larger values produce
            // more accurate results, but may cause problems with hardware
            // acceleration. But there are workarounds for that, too; refer to
            // http://stackoverflow.com/questions/6253528/font-size-too-large-to-fit-in-cache
            const float testTextSize = 48f;

            // Get the bounds of the text, using our testTextSize.
            paint.TextSize = testTextSize;
            Rect bounds = new Rect();
            paint.GetTextBounds(text, 0, text.Length, bounds);

            // Calculate the desired size as a proportion of our testTextSize.
            float desiredTextSize = testTextSize * desiredHeight / bounds.Height();

            // Set the paint for that size.
            paint.TextSize = desiredTextSize;
        }

		protected override void OnDraw(Canvas canvas)
		{
			// For some reason this knows its size; OnSizeChanged is never called.
			//Logr.D ("Cell.onDraw {0} {1}", this.Width, this.Height); //154x154 on Nexus 5

			Paint PaintDot = new Paint (); 
			PaintDot.SetStyle (Android.Graphics.Paint.Style.Fill);

			Paint paintDay = new Paint {
				AntiAlias = true,
				Color = Android.Graphics.Color.Black,
				//TextSize = (float) Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				FakeBoldText = true
			};
			//SetTextSizeForWidth (paintDay, this.Width/6.0f, "8");
            SetTextSizeForHeight (paintDay, this.Height/3.0f, "8");
			canvas.DrawText (this.Text, this.Width*0.1f/*canvas.ClipBounds.Right*0.1f*/, this.Height*0.4f/*canvas.ClipBounds.Bottom * 0.35f*/, paintDay);

			if (dot1) 
			{
				PaintDot.Color = Android.Graphics.Color.Red;
				canvas.DrawCircle (canvas.ClipBounds.Right * 0.65f, canvas.ClipBounds.Bottom * 0.15f, 7, PaintDot);
			}
			if (dot2) 
			{
				PaintDot.Color = Android.Graphics.Color.Red;
				canvas.DrawCircle (canvas.ClipBounds.Right * 0.85f, canvas.ClipBounds.Bottom * 0.15f, 7, PaintDot);
			}
			if (dot3) 
			{
				PaintDot.Color = Android.Graphics.Color.Red;
				canvas.DrawCircle (canvas.ClipBounds.Right * 0.65f, canvas.ClipBounds.Bottom * 0.35f, 7, PaintDot);
			}
			if (dot4) 
			{
				PaintDot.Color = Android.Graphics.Color.Red;
				canvas.DrawCircle (canvas.ClipBounds.Right * 0.85f, canvas.ClipBounds.Bottom * 0.35f, 7, PaintDot);
			}

			if (ShowLabel) 
			{
				Paint paintBlockSchedule = new Paint {
					AntiAlias = true,
					Color = Android.Graphics.Color.Black,
					//UnderlineText = true,
					//TextSize = 30f //(float) Device.GetNamedSize(NamedSize.Large, typeof(Label))
				};
				SetTextSizeForWidth (paintBlockSchedule, this.Width/1.4f, "88 8888");
                canvas.DrawText (LabelText, this.Width * 0.1f, this.Height * 0.8f, paintBlockSchedule);
			}

		}

		bool dot1 = false;
		bool dot2 = false;
		bool dot3 = false;
		bool dot4 = false;

		public void DrawDots(int Dots)
		{
			switch (Dots) 
			{
			case 0: 
				dot1 = true;
				dot2 = true;
				dot3 = true;
				dot4 = true;
				break;
			case 1:
				dot1 = true;
				dot2 = true;
				dot3 = true;
				break;
			case 2:
				dot1 = true;
				dot2 = true;
				break;
			case 3:
				dot1 = true;
				break;
			case 4:
				break;
			}
			this.Invalidate ();
		}

		bool ShowLabel = false;
		string LabelText = "";
		public void DrawLabel(string EventName)
		{
			ShowLabel = true;
			LabelText = EventName;
			this.Invalidate ();
		}
	}
}
