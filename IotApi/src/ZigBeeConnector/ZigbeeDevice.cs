﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZigBeeConnector
{
    private Adapter m_adapter = null;
    public Adapter ZigBeeAdatper
    {
        get { return m_adapter; }
    }
    public XBeeModule Module
    {
        get { return m_adapter.XBeeModule; }
    }

    public UInt16 NetworkAddress { get; set; }

    private UInt64 m_macAddress = AdapterHelper.UNKNOWN_MAC_ADDRESS;
    public UInt64 MacAddress
    {
        get { return m_macAddress; }
    }

    private string m_name = null;
    public string Name
    {
        get { return m_name; }
    }

    bool m_isEndDevice = true;
    public bool IsEndDevice
    {
        get { return m_isEndDevice; }
    }

    private Dictionary<byte, ZigBeeEndPoint> m_endPointList = new Dictionary<byte, ZigBeeEndPoint>();
    public Dictionary<byte, ZigBeeEndPoint> EndPointList
    {
        get { return m_endPointList; }
    }

    public bool AddClusterToEndPoint(bool isInCluster, byte endPointId, UInt16 profileId, UInt16 deviceId, UInt16 clusterId, Adapter adapter)
    {
        bool newEndPoint = false;

        // sanity check
        if (adapter == null &&
            m_adapter == null)
        {
            // can't do anything without XBeeModule
            return false;
        }

        // save away XBeeModule if necessary
        if (m_adapter == null)
        {
            m_adapter = adapter;
        }

        // find relevant end point (create if if necessary)
        ZigBeeEndPoint endPoint = null;
        if (!m_endPointList.TryGetValue(endPointId, out endPoint))
        {
            endPoint = new ZigBeeEndPoint(endPointId, profileId, deviceId);
            endPoint.Initialize(this);
            newEndPoint = true;
        }

        // add cluster to end point
        if (!endPoint.AddCluster(clusterId, isInCluster))
        {
            return false;
        }

        // add new end point to end point list
        if (newEndPoint)
        {
            m_endPointList.Add(endPoint.Id, endPoint);
        }
        return true;
    }

    public ZigBeeDevice(UInt16 networkAddress, UInt64 macAddress, bool isEndDevice = true)
    {
        NetworkAddress = networkAddress;
        m_macAddress = macAddress;
        m_isEndDevice = isEndDevice;
    }
}
}
