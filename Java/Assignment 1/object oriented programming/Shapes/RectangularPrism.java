package oop.Shapes;

public class RectangularPrism implements Shape {

    private double width;
    private double height;
    private double length;
    
    public double volume() {
        return width * length * height;
    }

    public double surfaceArea() {
        return 2 * (width * length + height * length + height * width);
    }
    
    RectangularPrism(double width, double height, double length) {
        this.width = width;
        this.height = height;
        this.length = length;
    }

}
