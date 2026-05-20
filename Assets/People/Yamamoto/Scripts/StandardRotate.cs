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
        var ParentTransform = transform.parent;
        var currentRotation = ParentTransform.rotation.eulerAngles;
        ParentTransform
            .DORotate(
                new Vector3(
                    currentRotation.x,
                    currentRotation.y,
                    currentRotation.z += _rotateAngle * -dir),
                _rotateDuration)
            .SetEase(_rotateEase);
    }
}
