using DG.Tweening;
using UnityEngine;

public class StandardRotate : GimmickBase
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    protected override void Rotate(int dir)
    {
        Transform ParentTransform = GetComponentInParent<Transform>();
        var currentRotation = ParentTransform.rotation.eulerAngles;
        transform
            .DORotate(
                new Vector3(
                    currentRotation.x,
                    currentRotation.y,
                    currentRotation.z += _rotateAngle * -dir),
                _rotateDuration)
            .SetEase(_rotateEase);
    }
}
