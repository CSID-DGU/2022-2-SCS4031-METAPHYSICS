using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public static float SoundMaxValue = 100.0f;
    public static float SoundMinValue = 0.0f;

    [SerializeField]
    Button m_AlarmCheckBtn = null;

    [SerializeField]
    Slider m_AlarmHandleSlider = null;

    [SerializeField]
    Sprite m_AlarmEnableImg = null;

    [SerializeField]
    Sprite m_AlarmDisableImg = null;

    bool m_AlarmEnable = true;

    [SerializeField]
    Slider m_SoundBar = null;

    [SerializeField]
    Button m_SoundPlusBtn = null;

    [SerializeField]
    Button m_SoundMinusBtn = null;

    [SerializeField]
    Slider m_EffectSoundBar = null;

    [SerializeField]
    Button m_EffectSoundPlusBtn = null;

    [SerializeField]
    Button m_EffectSoundMinusBtn = null;

    [SerializeField]
    Button m_SaveBtn = null;

    void Start()
    {
        //초기값 임의 지정
        m_AlarmHandleSlider.maxValue = 0.3f;
        m_AlarmHandleSlider.minValue = 0.0f;

        //친구매니저의 알람설정 가져오기
        m_AlarmEnable = Managers.Data.m_FriendManager.AlarmEnable;

        if (m_AlarmEnable)
        {
            m_AlarmHandleSlider.value = m_AlarmHandleSlider.maxValue;
            m_AlarmCheckBtn.image.sprite = m_AlarmEnableImg;
        }

        else
        {
            m_AlarmHandleSlider.value = m_AlarmHandleSlider.minValue;
            m_AlarmCheckBtn.image.sprite = m_AlarmDisableImg;
        }


        m_SoundBar.maxValue = SoundMaxValue;
        m_SoundBar.minValue = SoundMinValue;

        m_EffectSoundBar.maxValue = SoundMaxValue;
        m_EffectSoundBar.minValue = SoundMinValue;

        m_SoundBar.value = Managers.Sound.MasterSoundVolume;
        m_EffectSoundBar.value = Managers.Sound.EffectSoundVolume;
    }

    void Update()
    {
        MoveAlarmHandle();
    
    }

    public void SoundPlusButtonCallback()
    {
        float SoundValue = m_SoundBar.value;

        SoundValue += 10.0f;

        if (SoundValue > SoundMaxValue)
            SoundValue = SoundMaxValue;

        m_SoundBar.value = SoundValue;
    }
    public void SoundMinusButtonCallback()
    {
        float SoundValue = m_SoundBar.value;

        SoundValue -= 10.0f;

        if (SoundValue < SoundMinValue)
            SoundValue = SoundMinValue;

        m_SoundBar.value = SoundValue;
    }
    public void EffectSoundPlusButtonCallback()
    {
        float SoundValue = m_EffectSoundBar.value;

        SoundValue += 10.0f;

        if (SoundValue > SoundMaxValue)
            SoundValue = SoundMaxValue;

        m_EffectSoundBar.value = SoundValue;

    }
    public void EffectSoundMinusButtonCallback()
    {
        float SoundValue = m_EffectSoundBar.value;

        SoundValue -= 10.0f;

        if (SoundValue < SoundMinValue)
            SoundValue = SoundMinValue;

        m_EffectSoundBar.value = SoundValue;
    }

    public void AlarmCheckButtonCallback()
    {
        m_AlarmEnable = m_AlarmEnable ? false : true;

        //버튼 이미지 바꿔주기
        if (m_AlarmEnable)
            m_AlarmCheckBtn.image.sprite = m_AlarmEnableImg;

        else
            m_AlarmCheckBtn.image.sprite = m_AlarmDisableImg;

        //데이터 매니저에 알람정보 넘겨주기
    }

    public void SaveButtonCallback()
    {
        Managers.Data.m_FriendManager.AlarmEnable = m_AlarmEnable;

        Managers.Sound.MasterSoundVolume = m_SoundBar.value;
        Managers.Sound.EffectSoundVolume = m_EffectSoundBar.value;
        //사운드 설정
    }

    void MoveAlarmHandle()
    {
        if (m_AlarmEnable)
        {
            if (m_AlarmHandleSlider.value < m_AlarmHandleSlider.maxValue)
                m_AlarmHandleSlider.value += Time.deltaTime;
        }

        else
        {
            if (m_AlarmHandleSlider.value > m_AlarmHandleSlider.minValue)
                m_AlarmHandleSlider.value -= Time.deltaTime;
        }
    }
}
