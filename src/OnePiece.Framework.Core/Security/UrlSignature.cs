using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core.Security
{
    public class UrlSignature : IUrlSignature
    {
        public IRequestRepository RequestRepository { get; set; }
        public Encoding Encoding { get; set; }
        public SignatureContext Context { get; set; }
        public ISignature Signature { get; set; }

        public UrlSignature(IRequestRepository requestRepository, Encoding encoding)
        {
            this.RequestRepository = requestRepository;
            this.Encoding = encoding;
            this.Context = new SignatureContext(RequestRepository, Encoding);
            this.Signature = SignatureFactory.GetSignature(Context.Method);
        }

        public UrlSignature(IRequestRepository requestRepository, SignatureContext context)
        {
            this.Context = context;
            this.Encoding = Encoding.UTF8;
            this.Signature = SignatureFactory.GetSignature(context.Method);
            this.RequestRepository = requestRepository;
        }

        public bool IsValid()
        {
            var isValid = false;

            if (!Context.Secret.IsNullOrEmpty())
            {
                var signature = this.Sign(Context);

                if (signature.EqualsOrdinalIgnoreCase(Context.ClientSignature))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public string Sign()
        {
            return this.Signature.Sign(this.Context);
        }

        public string Sign(SignatureContext context)
        {
            return this.Signature.Sign(context);
        }
    }
}
