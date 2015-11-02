﻿using System;

namespace Criteo.Profiling.Tracing.Tracers.Zipkin
{
    internal class ZipkinAnnotation
    {
        private readonly DateTime timestamp;

        public string Value { get; private set; }

        public ZipkinAnnotation(DateTime timestamp, string value)
        {
            this.timestamp = timestamp;
            this.Value = value;
        }

        public Thrift.Annotation ToThrift()
        {
            var thriftAnn = new Thrift.Annotation()
            {
                Timestamp = ToUnixTimestamp(this.timestamp),
                Value = this.Value
            };

            return thriftAnn;
        }

        public override string ToString()
        {
            return String.Format("ZipkinAnn: ts={0} val={1}", ToUnixTimestamp(timestamp), Value);
        }

        /// <summary>
        /// Create a UNIX timestamp from a UTC date time. Time is expressed in microseconds and not seconds.
        /// </summary>
        /// <see href="https://en.wikipedia.org/wiki/Unix_time"/>
        /// <param name="utcDateTime"></param>
        /// <returns></returns>
        internal static long ToUnixTimestamp(DateTime utcDateTime)
        {
            return (long)((utcDateTime.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds) * 1000L;
        }
    }
}