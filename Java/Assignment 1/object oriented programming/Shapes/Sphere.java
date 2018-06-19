package oop.Shapes;

public class Sphere implements Shape {

    private double radius;
    
    public double volume() {
        return (4 * Math.PI * Math.pow(radius, 3)) / 3;
    }

    public double surfaceArea() {
        return 4 * Math.PI * Math.pow(radius, 2);
    }
    
    public Sphere (double radius) {
        this.radius = radius;
    }

}
