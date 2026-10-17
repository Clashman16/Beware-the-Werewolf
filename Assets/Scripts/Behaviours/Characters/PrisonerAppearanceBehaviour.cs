using BWW.Behaviours.Characters;
using UnityEngine;

public class PrisonerAppearanceBehaviour : HumanAppearanceBehaviour
{
    private int m_dTextureId;

    public int TextureId
    {
        get => m_dTextureId;
        set => m_dTextureId = value;
    }

    public override void UpdateAppearance(bool p_bIsCharacterFemale)
    {
        IsCharacterFemale = p_bIsCharacterFemale;

        string l_sTextureKey = string.Concat("skin_h", IsCharacterFemale ? "er_" : "im_", "poor_", m_dTextureId.ToString());

        Texture2D l_text = GetTextureFromKey(l_sTextureKey);

        ApplyTexture(l_text);
    }
}
