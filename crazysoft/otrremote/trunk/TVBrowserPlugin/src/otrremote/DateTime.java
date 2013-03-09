package otrremote;

import java.util.Calendar;
import java.util.Locale;
import java.util.TimeZone;

public class DateTime implements Cloneable {
	int month;
	int day;
	int year;
	int hour;
	int min;
	int sec;

	public DateTime() {
		TimeZone tz = TimeZone.getTimeZone("GMT");
		Calendar rightNow = Calendar.getInstance(tz);

		this.year = rightNow.get(1);
		this.month = (rightNow.get(2) + 1);
		this.day = rightNow.get(5);
		this.hour = rightNow.get(11);
		this.min = rightNow.get(12);
		this.sec = rightNow.get(13);
	}

	public DateTime(int yr, int mo, int da, int hr, int mn, int sc) {
		this.year = yr;
		this.month = mo;
		this.day = da;
		this.hour = hr;
		this.min = mn;
		this.sec = sc;

		if (!isValid())
			throw new IllegalArgumentException();
	}

	public DateTime(int yr, int mo, int da) {
		this(yr, mo, da, 0, 0, 0);
	}

	public DateTime(Calendar inCalendar) {
		this.year = inCalendar.get(1);
		this.month = (inCalendar.get(2) + 1);
		this.day = inCalendar.get(5);
		this.hour = inCalendar.get(11);
		this.min = inCalendar.get(12);
		this.sec = inCalendar.get(13);

		TimeZone tz = inCalendar.getTimeZone();
		int off = tz.getRawOffset();

		advanceSecs(-off / 1000);
	}

	public DateTime(int julian) {
		fromJulian(julian);
		if (!isValid())
			throw new IllegalArgumentException();
	}

	/** @deprecated */
	public void advance(int n) {
		advanceDays(n);
	}

	public void advanceDays(int n) {
		fromJulian(toJulian() + n);
	}

	public void advanceHours(int hr) {
		double addval = hr;
		addval /= 24.0D;
		fromJulian(toJulian() + addval);
	}

	public void advanceMins(int min) {
		double addval = min;
		addval = addval / 60.0D / 24.0D;
		fromJulian(toJulian() + addval);
	}

	public void advanceSecs(int sec) {
		double addval = sec;
		addval = addval / 60.0D / 60.0D / 24.0D;
		fromJulian(toJulian() + addval);
	}

	public int getDay() {
		return this.day;
	}

	public int getMonth() {
		return this.month;
	}

	public int getYear() {
		return this.year;
	}

	public int getHour() {
		return this.hour;
	}

	public int getMin() {
		return this.min;
	}

	public int getSecs() {
		return this.sec;
	}

	public int weekday() {
		return ((int) toJulian() + 1) % 7;
	}

	public int daysBetween(DateTime other) {
		int ithis = (int) toJulian();
		int iother = (int) other.toJulian();
		return ithis - iother;
	}

	public Object clone() {
		try {
			return super.clone();
		} catch (CloneNotSupportedException e) {
		}
		return null;
	}

	public String toString() {
		return getYear() + "-" + getMonth() + "-" + getDay() + " " + getHour()
				+ ":" + getMin() + ":" + getSecs();
	}

	public boolean isValid() {
		DateTime dt = new DateTime();

		dt.fromJulian(toJulian());

		return (dt.day == this.day) && (dt.month == this.month)
				&& (dt.year == this.year) && (dt.hour == this.hour)
				&& (dt.min == this.min) && (dt.sec == this.sec);
	}

	public double toJulian() {
		int y = this.year;
		int m = this.month;

		int IGREG = 588829;

		if (y < 0)
			y += 1;
		if (m > 2) {
			m += 1;
		} else {
			y -= 1;
			m += 13;
		}

		int ijulian = (int) (365.25D * y) + (int) (30.600100000000001D * m)
				+ this.day + 1720995;

		if (this.day + 31 * (m + 12 * y) >= IGREG) {
			int adj = y / 100;
			ijulian = ijulian + 2 - adj + adj / 4;
		}

		double timeofday = this.hour / 24.0D + this.min / 1440.0D + this.sec
				/ 86400.0D;
		return ijulian + timeofday;
	}

	public void fromJulian(double injulian) {
		int JGREG = 2299161;
		double halfsecond = 0.5D;

		double julian = injulian + halfsecond / 86400.0D;

		int ja = (int) julian;

		if (ja >= JGREG) {
			int jalpha = (int) ((ja - 1867216 - 0.25D) / 36524.25D);
			ja = ja + 1 + jalpha - jalpha / 4;
		}

		int jb = ja + 1524;
		int jc = (int) (6680.0D + (jb - 2439870 - 122.09999999999999D) / 365.25D);
		int jd = 365 * jc + jc / 4;
		int je = (int) ((jb - jd) / 30.600100000000001D);
		this.day = (jb - jd - (int) (30.600100000000001D * je));
		this.month = (je - 1);
		if (this.month > 12)
			this.month -= 12;
		this.year = (jc - 4715);
		if (this.month > 2)
			this.year -= 1;
		if (this.year <= 0)
			this.year -= 1;

		double thetime = julian - (int) julian;
		thetime *= 24.0D;
		this.hour = (int) thetime;
		thetime = (thetime - this.hour) * 60.0D;
		this.min = (int) thetime;
		thetime = (thetime - this.min) * 60.0D;
		this.sec = (int) thetime;
	}

	public void fromPIDTime(long thepidtime) {
		double jul = thepidtime;
		jul = jul / 10000000.0D / 60.0D / 60.0D / 24.0D;
		fromJulian(jul);
	}

	public long toPIDTime() {
		double jul = toJulian();
		long pidtime = (long) (jul * 24.0D * 60.0D * 60.0D * 10000000.0D);
		return pidtime;
	}

	public Calendar makeGMTCalendar() {
		Calendar gmtCal = Calendar.getInstance(TimeZone.getTimeZone("GMT"),
				Locale.getDefault());
		gmtCal.set(this.year, this.month - 1, this.day, this.hour, this.min,
				this.sec);
		return gmtCal;
	}

	public Calendar makeLocalCalendar() {
		Calendar locCal = Calendar.getInstance();
		TimeZone tz = locCal.getTimeZone();
		int off = tz.getRawOffset() / 1000;
		advanceSecs(off);
		locCal.set(this.year, this.month - 1, this.day, this.hour, this.min,
				this.sec);
		advanceSecs(-off);
		return locCal;
	}
}