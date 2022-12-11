using System;

namespace XMPP
{
    internal class ContextCallOnlyOnce
    {
        public static Action<string> CallOnlyOnce(Action<string> action)
        {
            var context = new ContextCallOnlyOnce();
            Action<string> ret = (string a) =>
            {
                if (false == context.AlreadyCalled)
                {
                    action(a);
                    context.AlreadyCalled = true;
                }
            };

            return ret;
        }

        public bool AlreadyCalled;

    }
}
