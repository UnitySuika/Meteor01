using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject gun;

    [SerializeField] GameObject beamPrefab;

    [SerializeField] float beamMaxThick;
    [SerializeField] float beamFadeTime;

    TurretInput turretInput;

    const float beamLength = 100f;

    public Action<Meteor> HitMeteor;

    private void Start()
    {
        turretInput = GetComponent<TurretInput>();

        turretInput.Shoot += Shoot;
    }

    private void Update()
    {
        gun.transform.eulerAngles = new Vector3(0, 0, turretInput.GetAngle());

        float r = Mathf.Deg2Rad * (gun.transform.eulerAngles.z + 90f);
        Vector3 dir = new Vector3(Mathf.Cos(r), Mathf.Sin(r));
        Debug.DrawLine(gun.transform.position, gun.transform.position + dir * 100f);
    }

    void Shoot()
    {
        // 方向ベクトルを計算
        float rot = Mathf.Deg2Rad * (gun.transform.eulerAngles.z + 90f); // + 90f は補正
        Vector3 dir = new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0);

        Vector3 gunTip = gun.transform.Find("TipPoint").position;
        Vector3 target = gunTip + dir * beamLength;

        // ビーム生成
        GameObject beam = Instantiate(beamPrefab);
        beam.transform.position = (target + gunTip) / 2f;
        beam.transform.eulerAngles = gun.transform.eulerAngles;
        beam.transform.localScale = new Vector3(beamMaxThick, beamLength, 0);


        // ビームに当たった隕石を消す
        RaycastHit2D[] hits = Physics2D.BoxCastAll(beam.transform.position, beam.transform.localScale, beam.transform.eulerAngles.z, Vector2.zero);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.TryGetComponent(out Meteor meteor))
                {
                    HitMeteor(meteor);
                    meteor.Break();
                }
            }
        }

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

            SpriteRenderer renderer = beam.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = Mathf.Lerp(1, 0, time / beamFadeTime);
            renderer.color = color;

            yield return null;
        }

        Destroy(beam.gameObject);
    }
}
