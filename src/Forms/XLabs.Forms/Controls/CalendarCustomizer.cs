using System;
using Xamarin.Forms; // Color
using System.Collections.Generic; // List

namespace XLabs.Forms.Controls
{
	public class CalendarEvent
	{
		public int id { get; set; }
		public string name { get; set; }
	}

	public class CalendarEventCategory
	{
		public int calMode { get; set;}
		public Color calColor { get; set;}
	}

	public interface ICalendarCustomizer
	{
		List<CalendarEvent> GetEventsForDay (int month, int day);
	}
}

