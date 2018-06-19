package oop.Shapes;

public class Cylinder implements Shape {

    private double radius;
    private double height;
    
    public double volume() {
        return Math.PI * height * Math.pow(radius, 2);
    }
    
    public double surfaceArea() {
        return 2 * Math.PI * radius * height + 2 * Math.PI * Math.pow(radius, 2);
    }
    
    Cylinder(double radius, double height) {
        this.radius = radius;
        this.height = height;
    }

}
