using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public class GarciaStringBuilder
    {
        protected StringBuilder Builder { get; set; }

        public GarciaStringBuilder(string value)
        {
            this.Builder = new StringBuilder(value);
        }

        public GarciaStringBuilder(params string[] value) : this('\0', value)
        {
        }

        public GarciaStringBuilder(char seperator, params string[] value)
        {
            if (value != null && value.Length != 0)
            {
                this.Builder = new StringBuilder(value[0]);

                if (value.Length > 1)
                {
                    for (int i = 1; i < value.Length; i++)
                    {
                        if (seperator != '\0' && i != value.Length - 1)
                        {
                            this.Builder.Append(seperator);
                        }

                        this.Builder.Append(value[i]);
                    }
                }
            }
            else
            {
                this.Builder = new StringBuilder();
            }
        }

        public GarciaStringBuilder()
            : this(string.Empty)
        {
        }

        public void Append(string value)
        {
            this.Builder.Append(value);
        }

        public void Append(char value)
        {
            this.Builder.Append(value);
        }

        public void Append(bool value)
        {
            this.Builder.Append(value);
        }

        public void Append(int value)
        {
            this.Builder.Append(value);
        }

        public void Append(double value)
        {
            this.Builder.Append(value);
        }

        public void Append(long value)
        {
            this.Builder.Append(value);
        }

        public void Append(short value)
        {
            this.Builder.Append(value);
        }

        public void Append(byte value)
        {
            this.Builder.Append(value);
        }

        public void Append(char[] value)
        {
            this.Builder.Append(value);
        }

        public void Append(decimal value)
        {
            this.Builder.Append(value);
        }

        public void Append(float value)
        {
            this.Builder.Append(value);
        }

        public void Append(object value)
        {
            this.Builder.Append(value);
        }

        public void Append(char[] value, int startIndex, int charCount)
        {
            this.Builder.Append(value, startIndex, charCount);
        }

        public void Append(string value, int startIndex, int count)
        {
            this.Builder.Append(value, startIndex, count);
        }

        public static GarciaStringBuilder operator +(GarciaStringBuilder builder, string text)
        {
            builder.Builder.Append(text);
            return builder;
        }

        public static GarciaStringBuilder operator +(GarciaStringBuilder builder, char text)
        {
            builder.Builder.Append(text);
            return builder;
        }

        public override string ToString()
        {
            return this.Builder.ToString();
        }
    }
}
