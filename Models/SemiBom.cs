namespace Models
{
    public class SemiBom:DrawingNumMatrix
    {
        public int SemiBomId { get; set; }
        public int ProjectId { get; set; }        
        public int Quantity { get; set; }       
    }
}
