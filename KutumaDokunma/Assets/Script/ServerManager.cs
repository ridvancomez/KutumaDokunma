using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Runtime.InteropServices;
using TMPro;

public class ServerManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Servere Bağlandı");
        PhotonNetwork.JoinLobby();
    }

    #region Giris Islemleri
    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye Girdi");
    }

    public override void OnJoinedRoom()
    {
        GameObject objem = PhotonNetwork.Instantiate("Oyuncu", Vector3.zero, Quaternion.identity,0, null);

        objem.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("UserName");


        InvokeRepeating("ChekInformation", 0, .5f);
    }
    #endregion


    #region Cikis Islemleri
    public override void OnLeftRoom()
    {
        //Odadan Çıkma
    }

    public override void OnLeftLobby()
    {
        //Lobiden Çıkma
    }
    #endregion


    #region Oyuncu Odadan Girip Ciktiginda
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        //Bir oyuncu Girdiğinde
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        //Bir oyuncu çıktığında
        InvokeRepeating("ChekInformation", 0, 1);
    }
    #endregion


    #region Hata Islemleri
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //Odaya Girilemediyse
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //Rastgele Bir Odaya Girilemediyse
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //Oda Oluşturulamadıysa
    }
    #endregion


    #region Buton Islemleri
    public void RandomLogin()
    {
        PhotonNetwork.JoinRandomRoom();

        PhotonNetwork.LoadLevel(1);
    }

    public void CreateRoomAndLogin()
    {
        PhotonNetwork.LoadLevel(1);

        int roomName = Random.Range(0, 9964124);
        PhotonNetwork.JoinOrCreateRoom(roomName.ToString(), new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }
    #endregion


    private void ChekInformation()
    {

        if(PhotonNetwork.PlayerList.Length == 2)
        {
            GameObject.FindGameObjectWithTag("OyuncuBekleniyor").SetActive(false);

            GameObject.FindGameObjectWithTag("Oyuncu1Isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindGameObjectWithTag("Oyuncu2Isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[1].NickName;

            CancelInvoke("ChekInformation");
        }
        else
        {
            GameObject.FindGameObjectWithTag("OyuncuBekleniyor").SetActive(true);

            GameObject.FindGameObjectWithTag("Oyuncu1Isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindGameObjectWithTag("Oyuncu2Isim").GetComponent<TextMeshProUGUI>().text = "..........";
        }
    }



}