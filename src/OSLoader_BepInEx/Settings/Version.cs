using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OSLoader
{
    public class Version
    {
        public readonly bool valid;
        public readonly bool empty;
        private readonly int major;
        private readonly int minor;
        private readonly int patch;

        public Version(string rawVersion)
        {
            if (string.IsNullOrEmpty(rawVersion))
            {
                empty = true;
                major = 0;
                minor = 0; 
                patch = 0;
                return;
            }

            string[] subVersions = rawVersion.Split('.');

            // Malformed version
            if (subVersions.Length > 3) return;
            if (subVersions.Where(e => ContainsNonNumeric(e)).Count() != 0) return;

            major = int.Parse(subVersions[0]);
            if (subVersions.Length >= 2) minor = int.Parse(subVersions[1]);
            if (subVersions.Length >= 3) patch = int.Parse(subVersions[2]);
            valid = true;
        }

        private bool ContainsNonNumeric(string str)
        {
            return str.ToLower().ToCharArray().Where(c => !(c >= '0' && c <= '9')).Count() != 0;
        }

        public override string ToString()
        {
            return $"{major}.{minor}.{patch}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Version version)) return false;
            return this == version;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator >(Version a, Version b)
        {
            if (a.major > b.major) return true;
            if (a.minor > b.minor) return true;
            if (a.patch > b.patch) return true;
            return false;
        }

        public static bool operator ==(Version a, Version b) => a.major == b.major && a.minor == b.minor && a.patch == b.patch;

        public static bool operator !=(Version a, Version b) => !(a == b);

        public static bool operator >=(Version a, Version b) => a > b || a == b;

        public static bool operator <=(Version a, Version b) => !(a > b);

        public static bool operator <(Version a, Version b) => !(a >= b);
    }
}
