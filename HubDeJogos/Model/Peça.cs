using Hub.Model.Peças;

namespace Hub.Model
{
    public class Peça
    {
        public  PeaoPreto PeaoPreto { get; set; }
        public PeaoBranco PeaoBranco { get; set; }

        public Peça()
        {
            PeaoBranco= new PeaoBranco();
            PeaoPreto=new PeaoPreto();
        }
    }
}