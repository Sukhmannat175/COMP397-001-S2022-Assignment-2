/*  Filename:           Vector3Surrogate.cs
 *  Author:             Sukhmannat Singh (301168420)
 *  Last Update:        June 26, 2022
 *  Description:        Vector 3 Surrogate, for save functionality
 *  Revision History:   June 26, 2022 (Sukhmannat Singh): Initial script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[DataContract(Name = "Vector3")]
public class Vector3Surrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        Vector3 vector = (Vector3)obj;
        info.AddValue("x", vector.x);
        info.AddValue("y", vector.y);
        info.AddValue("z", vector.z);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Vector3 vector = (Vector3)obj;
        vector.x = (float)info.GetValue("x", typeof(float));
        vector.y = (float)info.GetValue("y", typeof(float));
        vector.z = (float)info.GetValue("z", typeof(float));
        obj = vector;

        return obj;
    }
}
