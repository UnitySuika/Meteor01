using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject gun;

    [SerializeField] GameObject beamPrefab;

    [SerializeField] float beamMaxThick;
    [SerializeField] float beamFadeTime;

    TurretInput turretInput;

    const float beamLength = 100f;

    private void Start()
    {
        turretInput = GetComponent<TurretInput>();

        turretInput.Shoot += Shoot;
    }


    private void Update()
    {
        gun.transform.eulerAngles = new Vector3(0, 0, turretInput.GetRotation());

        float r = Mathf.Deg2Rad * (gun.transform.eulerAngles.z + 90f);
        Vector3 dir = new Vector3(Mathf.Cos(r), Mathf.Sin(r));
        Debug.DrawLine(gun.transform.position, gun.transform.position + dir * 100f);
    }

    void Shoot()
    {
        // �����x�N�g�����v�Z
        float rot = Mathf.Deg2Rad * (gun.transform.eulerAngles.z + 90f); // + 90f �͕␳
        Vector3 dir = new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0);

        // �������΂��ē�������覐΂�����
        Vector3 gunTip = gun.transform.Find("TipPoint").position;
        Vector3 target = gunTip + dir * beamLength;
        RaycastHit2D hit = Physics2D.Linecast(gunTip, target);
        if (hit && hit.collider.gameObject.TryGetComponent(out Meteor meteor))
        {
            Destroy(meteor.gameObject);
        }

        // �r�[������
        GameObject beam = Instantiate(beamPrefab);
        beam.transform.position = (target + gunTip) / 2f;
        beam.transform.eulerAngles = gun.transform.eulerAngles;
        beam.transform.localScale = new Vector3(beamMaxThick, beamLength, 0);
        StartCoroutine(BeamFadeOut(beam));
    }

    IEnumerator BeamFadeOut(GameObject beam)
    {
        float time = 0;
        Vector3 scale = beam.transform.localScale;

        while (scale.x > 0)
        {
            time += Time.deltaTime;

            scale.x = Mathf.Lerp(beamMaxThick, 0, time / beamFadeTime);
            beam.transform.localScale = scale;

            yield return null;
        }

        Destroy(beam.gameObject);
    }
}
