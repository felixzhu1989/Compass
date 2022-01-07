namespace Models
{
    public class LLKS : IModel
   {
       public int LLKSId { get; set; }
       public int ModuleTreeId { get; set; }
       //基本参数
       public double Length { get; set; }
       public int LongGlassNo { get; set; }
       public int ShortGlassNo { get; set; }
   }
}
