using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class PacketHandler
{   
    public static string m_packetHead = "<p/";
    public static string m_packetFoot = "/p>";
    public static string m_transformHead = "<t/";
    public static string m_transformFoot = "/t>";
    public static string m_gameStateHead = "<s/";
    public static string m_gameStateFoot = "/s>";
    public static string m_indexHead = "<i/";
    public static string m_indexFoot = "/i>";
    public static string m_elementHead = "<e/>";
    public static string m_elementFoot = "/e>";

    public static string m_modeTransform = "<m/trans/m>";
    public static string m_modeGameState = "<m/state/m>";


    // Mode: send object transform
    public static string makeElement(int _objIdx, string _objName, Transform _transform) {
        string element = m_elementHead + 
                        m_modeTransform +
                        m_indexHead + 
                        _objIdx.ToString() +
                        m_indexFoot + 
                        transform2Packet(_transform) +
                        m_elementFoot;
        return element;
    }
    public static string makeElement(string state) {
        string element = m_elementHead +
                        m_modeGameState + 
                        m_gameStateHead +
                        state + 
                        m_gameStateFoot +
                        m_elementFoot;
        return element;
    }
    public static string getLastPacket(string _message) {
        string[] packets = _message.Split(m_packetHead);
        string lastPacket = packets[packets.Length - 1];
        lastPacket = lastPacket.Replace(m_packetFoot, "");
        return lastPacket;
    }

    public static string[] parseElements(string _packet) {
        string[] eles = _packet.Split(m_elementHead);
        for(int i = 0; i < eles.Length; i++) {
            eles[i] = eles[i].Replace(m_elementFoot, "");
        }
        return eles;
    }

    public static int getPacketMode(string _packet) {
        Debug.Log(_packet);
        if(_packet.Contains(m_modeTransform)) return 1; // object transformation mode
        else if (_packet.Contains(m_modeGameState)) return 0; // game state
        else return -1;
    }
    public static int getObjectIndex(string _packet) {
        string[] pieces = _packet.Split(m_indexHead);
        string lastPiece = pieces[pieces.Length - 1];
        lastPiece = lastPiece.Split(m_indexFoot)[0];
        return int.Parse(lastPiece);
    }

    public static string getState(string _packet) {
        string[] pieces = _packet.Split(m_gameStateHead);
        string lastPiece = pieces[pieces.Length - 1];
        lastPiece = lastPiece.Split(m_gameStateFoot)[0];
        return lastPiece;
    }

    public static Pose packet2Pose(string _message) {
		string[] packets = _message.Split(m_transformHead);
		string lastPacket = packets[packets.Length - 1];
        lastPacket = lastPacket.Split(m_transformFoot)[0];
		string[] vals = lastPacket.Split(",");
		
		Vector3 pos = new Vector3(float.Parse(vals[0]), float.Parse(vals[1]), float.Parse(vals[2]));
		Quaternion rot = new Quaternion(float.Parse(vals[3]), float.Parse(vals[4]), float.Parse(vals[5]), float.Parse(vals[6]));
		return new Pose(pos, rot);
    }

    public static string transform2Packet(Transform _transform) {
        Vector3 pos = _transform.position;
        Quaternion rot = _transform.rotation;
        string packet = m_transformHead
                        + pos.x.ToString() + "," 
                        + pos.y.ToString() + "," 
                        + pos.z.ToString() + ","
                        + rot.x.ToString() + ","
                        + rot.y.ToString() + ","
                        + rot.z.ToString() + ","
                        + rot.w.ToString() + m_transformFoot;
        return packet;
    }
}

