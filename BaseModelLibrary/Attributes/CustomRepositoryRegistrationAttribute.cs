using System;
using System.Collections.Generic;
using System.Text;

namespace BaseModelLibrary.Attributes
{
    /// <summary>
    /// Mark interface to register it in services.
    /// Must have only one implemented version.
    /// </summary>
    public class CustomRepositoryRegistrationAttribute:Attribute
    {
    }
}
