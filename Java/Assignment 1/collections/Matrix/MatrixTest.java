package coll.Matrix;

public class MatrixTest {
    
    public static void main(String[] args) {
        
        Matrix<String> m = new Matrix<String>(2, 2);
        
        m.insert(0, 0, "a");
        m.insert(0, 1, "b");
        m.insert(1, 0, "c");
        m.insert(1, 1, "d");
        
        System.out.println(m.toString() + "\n");
        
        for (Object element : m) {
            System.out.println(element);
        }
        
    }

}
