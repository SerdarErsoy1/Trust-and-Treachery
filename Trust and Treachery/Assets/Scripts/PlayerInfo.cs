using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class PlayerInfo : Photon.Pun.MonoBehaviourPun, IPunObservable
{
    public int modelIndex;
    public GameObject playerBody; // Artık GameObject olarak kullanıyoruz

    public List<GameObject> _allPlayerModels = new List<GameObject>(); // Farklı body modelleri

    private void Awake()
    {
        if (photonView.IsMine)
        {
            // Eğer oyuncu kendine aitse rastgele bir model seç
            modelIndex = Random.Range(0, _allPlayerModels.Count);
            ChangePlayerBody(modelIndex);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Kendi verimizi gönderiyoruz
            stream.SendNext(modelIndex);
        }
        else
        {
            // Uzaktan gelen veriyi alıyoruz
            modelIndex = (int)stream.ReceiveNext();
            ChangePlayerBody(modelIndex);
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) // Eğer bu oyuncu diğer oyunculara aitse
        {
            ChangePlayerBody(modelIndex); // Modeli güncelle
        }
    }

    // Bu fonksiyon mevcut playerBody'yi yeni modelle değiştirir
    private void ChangePlayerBody(int index)
    {
        // Önceki playerBody'yi devre dışı bırak veya yok et
        if (playerBody != null)
        {
            Destroy(playerBody);
        }

        // Yeni modeli instantiate et ve player'ın altına yerleştir
        playerBody = Instantiate(_allPlayerModels[index], transform);
        playerBody.transform.localPosition = Vector3.zero; // Yeni modelin pozisyonunu ayarla
        playerBody.transform.localRotation = Quaternion.identity; // Rotasyonu sıfırla
    }
}