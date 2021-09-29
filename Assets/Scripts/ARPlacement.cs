using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject targetMarker;

    [SerializeField]
    private ARRaycastManager arRaycastManager;

    [SerializeField]
    private GameObject spider;

    private void Update()
    {
        /*
         * Posicionar o targetMarker no centro da visão da câmera
         * Emite um Raio a partir da câmera em direção à orientação do dispositivo
         * Ativar o targetMarker
         */

        var x = Screen.width / 2;
        var y = Screen.height / 2;

        var screenCenter = new Vector2(x, y);

        var hitResults = new List<ARRaycastHit>();

        var hit = arRaycastManager.Raycast(screenCenter, hitResults, TrackableType.Planes);

        if (hit)
        {
            var hitResult = hitResults[0];

            transform.position = hitResult.pose.position;
            transform.rotation = hitResult.pose.rotation;

            // Curiosidade: Há uma outra forma de definir posição e rotação na mesma operação (mais performático)
            // transform.SetPositionAndRotation(hitResult.pose.position, hitResult.pose.rotation);

            targetMarker.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0)
            && targetMarker.activeSelf)
        {
            var spiderClone = Instantiate(spider, transform.position, transform.rotation);

            spiderClone.SetActive(true);

            // spider.SetActive(true);
            //
            // spider.transform.position = transform.position;
            // spider.transform.rotation = transform.rotation;
        }
    }
}
