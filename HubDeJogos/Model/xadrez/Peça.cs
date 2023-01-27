using Hub.Model.xadrez.Peças;

namespace Hub.Model.xadrez
{
    public class Peça
    {
        public PeaoPreto PeaoPreto { get; set; }
        public PeaoBranco PeaoBranco { get; set; }

        public Peça()
        {
            PeaoBranco = new PeaoBranco();
            PeaoPreto = new PeaoPreto();
        }
    }
}