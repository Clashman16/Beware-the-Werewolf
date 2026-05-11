# Repository Structure

## Folders tree

The table below represent the folders tree from 'Assets' folder used in this repository:

<table>
  <tr>
    <th>Folder</th>
    <th>Subfolder</th>
    <th>Description</th>
  </tr>
  <tr>
    <td>Resources</td>
    <td>Animations</td>
    <td>Contains all Unity aimators and animations</td>
  </tr>
  <tr>
    <td>Resources</td>
    <td>Fonts</td>
    <td>Contains all fonts and TextMesh Pro fonts</td>
  </tr>
  <tr>
    <td>Resources</td>
    <td>Materials</td>
    <td>Contains all materials</td>
  </tr>
  <tr>
    <td>Resources</td>
    <td>Prefabs</td>
    <td>Contains all Unity prefabs</td>
  </tr>
  <tr>
    <td>Resources</td>
    <td>Shaders</td>
    <td>Contains all shaders</td>
  </tr>
  <tr>
    <td>Resources</td>
    <td>Sounds</td>
    <td>Contains all noises and musics</td>
  </tr>
  <tr>
    <td>Resources</td>
    <td>Sprites/UI</td>
    <td>Contains all sprites used by Graphic User Interface GameObjects</td>
  </tr>
  <tr>
    <td>Resources</td>
    <td>Text</td>
    <td>Contains all dialogs of the game</td>
  </tr>
  <tr>
    <td>Scenes</td>
    <td></td>
    <td>Contains all scenes of the game</td>
  </tr>
  <tr>
    <td>Scripts</td>
    <td>Behaviours</td>
    <td>Contains all scripts used attached on GameObjects</td>
  </tr>
  <tr>
    <td>Scripts</td>
    <td>Enum</td>
    <td>Contains all enums</td>
  </tr>
  <tr>
    <td>Scripts</td>
    <td>Managers</td>
    <td>Contains all scripts of objects that manage something in a scene</td>
  </tr>
  <tr>
    <td>Scripts</td>
    <td>ScriptableObjects</td>
    <td>Contains all scripts used to generate scriptable objects</td>
  </tr>
  <tr>
    <td>Scripts</td>
    <td>Utils</td>
    <td>Contains all utility scripts that are not attached to GameObject </td>
  </tr>
</table>

## Naming convention

The tables below represent the naming convention used in this repository:

<table>
  <tr>
    <th>Class</th>
  </tr>
  <tr>
    <th>Inherits MonoBehaviour</th>
    <th>Inherits MonoBehaviour and manages other GameObject</th>
  </tr>
  <tr>
    <td>MyClassBehaviour</td>
    <td>MyClassManager</td>
  </tr>
</table>

<table>
  <tr>
    <th>Variables</th>
  </tr>
  <tr>
    <th>Local</td>
    <th>Member</td>
    <th>Parameter</td>
  </tr>
  <tr>
    <td>l_aVariable</td>
    <td>m_myMember</td>
    <td>p_aParameter</td>
  </tr>
</table>

<table>
  <tr>
    <th>Types</th>
  </tr>
  <tr>
    <th>bool</th>
    <th>string</th>
    <th>int</th>
    <th>double</th>
    <th>float</th>
    <th>Transform</th>
    <th>GameObject</th>
    <th>RigidBody</th>
    <th>Image</th>
    <th>List<T></th>
    <th>T[]</th>
    <th>Dictionary<T,T></th>
    <th>Vector3</th>
  </tr>
  <tr>
    <td>bool m_bMyMember</td>
    <td>string m_sMyMember</td>
    <td>int m_dMyMember</td>
    <td>double m_dMyMember</td>
    <td>float m_fMyMember</td>
    <td>Transform m_trfMyMember</td>
    <td>GameObject m_goMyMember</td>
    <td>RigidBody m_rbMyMember</td>
    <td>Image m_imgMyMember</td>
    <td>List<T> m_lstMyMember</td>
    <td>T[] m_lstMyMember</td>
    <td>Dictionary<T,T> m_lstMyMember</td>
    <td>Vector3 m_vecMyMember</td>
  </tr>
</table>