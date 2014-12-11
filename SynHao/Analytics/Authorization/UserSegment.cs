﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics.Authorization
{
    public class UserSegment
    {
        private string _segmentId;
        private string _segmentName;

        public string SegmentId
        {
            get { return _segmentId; }
            set { _segmentId = value; }
        }

        public string SegmentName
        {
            get { return _segmentName; }
            set { _segmentName = value; }
        }
    }
}
