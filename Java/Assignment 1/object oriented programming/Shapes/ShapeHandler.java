package oop.Shapes;

public class ShapeHandler {
	// Returns the sum of the volumes of the given shapes.
	public static double volumeSum(Shape[] shapes) {
		double volumeTally = 0;
		
		for (int shape = 0; shape < shapes.length; shape++) {
		    volumeTally += shapes[shape].volume();
		}
	    
	    return volumeTally;
	}

	// Returns the sum of the surface areas of the given shapes.
	public static double surfaceAreaSum(Shape[] shapes) {
		double areaTally = 0;
		
		for (int shape = 0; shape < shapes.length; shape++) {
		    areaTally += shapes[shape].surfaceArea();
		}
		
	    return areaTally;
	}
}