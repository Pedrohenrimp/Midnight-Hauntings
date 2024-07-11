using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    #region Members
    [SerializeField]
    private bool mFollow = true;

    [SerializeField]
    private Transform mTarget;

    [SerializeField]
    private float mSmoothTime = 0.25f;

    [SerializeField]
    private float mVerticalOffset = 0;

    [SerializeField]
    private float mHorizontalOffset = 0;

    [SerializeField]
    private float mDeeplOffset = 0;

    [SerializeField]
    [Range(0, 360)]
    private float mCameraRotationX = 45;

    [SerializeField]
    [Range(0, 360)]
    private float mCameraRotationY = 180;

    [SerializeField]
    [Range(0, 360)]
    private float mCameraRotationZ = 0;

    [SerializeField]
    private Transform mTopLimit;

    [SerializeField]
    private Transform mDownLimit;

    [SerializeField]
    private Transform mLeftLimit;

    [SerializeField]
    private Transform mRightLimit;

    private Vector3 mOffset;
    private Vector3 mVelocity = Vector3.zero;
    #endregion

    #region Methods

    private async void Start()
    {
        var follow = mFollow;
        mFollow = false;
        mOffset = new Vector3(mHorizontalOffset, mVerticalOffset, mDeeplOffset);
        transform.DORotate(new Vector3(mCameraRotationX, mCameraRotationY, mCameraRotationZ), 3);
        transform.DOMove(mTarget.position + mOffset, 3);

        await Task.Delay(3000);
        mFollow = follow;
    }

    private void Update()
    {
        if(mFollow)
        {
    #if UNITY_EDITOR
            mOffset.x = mHorizontalOffset;
            mOffset.y = mVerticalOffset;
            mOffset.z = mDeeplOffset;

            transform.eulerAngles = new Vector3(mCameraRotationX, mCameraRotationY, mCameraRotationZ);
    #endif

            Vector3 targetPosition = mTarget.position + mOffset;

            //targetPosition.x = Mathf.Clamp(targetPosition.x, mLeftLimit.position.x + (mCameraDistance * 1.78f), mRightLimit.position.x - (mCameraDistance * 1.78f));
            //targetPosition.z = Mathf.Clamp(targetPosition.z, mDownLimit.position.z + mCameraDistance, mTopLimit.position.z - mCameraDistance);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref mVelocity, mSmoothTime);

        }
    }

    public void SetHorizontalOffsetDirection(float aSignal)
    {
        if (aSignal > 0)
        {
            mHorizontalOffset = Mathf.Abs(mHorizontalOffset);
            mOffset.x = mHorizontalOffset;

        }
        else if (aSignal < 0)
        {
            mHorizontalOffset = -Mathf.Abs(mHorizontalOffset);
            mOffset.x = mHorizontalOffset;
        }

    }
    #endregion
}
