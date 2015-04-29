namespace XLabs.Forms.Controls.MonoDroid.TimesSquare
{
	using System;

	using Android.Content;
	using Android.Runtime;
	using Android.Util;
	using Android.Widget;

	using Android.Graphics; // Paint

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

		protected override void OnDraw(Canvas canvas)
		{
			Paint PaintDot = new Paint (); 
			PaintDot.SetStyle (Android.Graphics.Paint.Style.Fill);

			Paint PaintDay = new Paint {
				AntiAlias = true,
				Color = Android.Graphics.Color.Black,
				TextSize = 30f,
				FakeBoldText = true
			};
			canvas.DrawText (this.Text, 10, 35, PaintDay);

			if (dot1) 
			{
				PaintDot.Color = Android.Graphics.Color.Red;
				canvas.DrawCircle (65, 15, 7, PaintDot);
			}
			if (dot2) 
			{
				PaintDot.Color = Android.Graphics.Color.Red;
				canvas.DrawCircle (85, 15, 7, PaintDot);
			}
			if (dot3) 
			{
				PaintDot.Color = Android.Graphics.Color.Red;
				canvas.DrawCircle (65, 35, 7, PaintDot);
			}
			if (dot4) 
			{
				PaintDot.Color = Android.Graphics.Color.Red;
				canvas.DrawCircle (85, 35, 7, PaintDot);
			}

			if (ShowLabel) 
			{
				Paint PaintBlockSchedule = new Paint {
					AntiAlias = true,
					Color = Android.Graphics.Color.Black,
					//UnderlineText = true,
					TextSize = 22f
				};
				canvas.DrawText (LabelText, 13, 90, PaintBlockSchedule);
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
