// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace CoreBot
{
    /// <summary>
    /// User state properties for Greeting.
    /// </summary>
    public class GreetingState
    {
        public string Name { get; set; }

        public string City { get; set; }

        public bool IsQuadrant { get; set; }
    }
}
