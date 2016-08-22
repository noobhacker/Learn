using Learn.Backend;
using Learn.Items;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Learn
{
    class Global
    {
        private static SkinItem userSkin;

        public SkinItem UserSkin
        {
            get
            {
                return userSkin;
            }
            set
            {
                if (value != userSkin)
                {
                    userSkin = value;
                }

            }
        }
        
    }
}
