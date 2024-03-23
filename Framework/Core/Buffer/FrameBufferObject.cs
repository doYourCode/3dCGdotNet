namespace Framework.Core.Buffer
{
    public class FrameBufferObject
    {
        /* -------------------------------------------- Variáveis de classe -------------------------------------------- */
#if DEBUG
        /// <summary>
        /// Representa o quantitativo de EBOs existentes na VRAM.
        /// </summary>
        public static UInt32 Count { get { return count; } private set { } }


        private static UInt32 count = 0;
#endif

        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// Id que reflete o endereço do buffer na VRAM
        /// </summary>
        public UInt32 ID { get { return id; } private set { } }

        
        private UInt32 id;

        private UInt32 textureID;       // Endereço da textura interna do FBO
                                        // TODO: trocar para a versão POO de textura que já está implementada (requer alterações)
        private UInt16 width, height;   // Altura e largura da textura produzida

        private UInt16 numSamples;        // Número de amostras para FBOs com anti-aliasing

        private float gamma;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        // TODO
    }
}
