public class Molecule {
    public static int[] Available = new int[5];
    
    public static void UpdateAvailable(int a, int b, int c, int d, int e) {
        Available = new int[]{a,b,c,d,e};
    }
}