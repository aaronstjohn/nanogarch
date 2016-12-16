package util;

/**
 * ...
 * @author ...
 */
class Gaussian
{
	private static var spare:Float;
	private static var isSpareReady:Bool;
	
	public static function getGaussian(mean:Float, stdDev:Float):Float
	{
		if (isSpareReady) {
			isSpareReady = false;
			return spare * stdDev + mean;
		} else {
			var u:Float;
			var v:Float;
			var s:Float;
			
			do {
				u = Math.random() * 2 - 1;
				v = Math.random() * 2 - 1;
				s = u * u + v * v;
			} while (s >= 1 || s == 0);
			
			var mul = Math.sqrt( -2 * Math.log(s) / s);
			spare = v * mul;
			isSpareReady = true;
			return mean + stdDev * u * mul;
		}
	}
}