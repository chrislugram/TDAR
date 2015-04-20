using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class UserManager {
	#region STATIC_ENUM_CONSTANTS
    public static readonly string PATH_RESOURCES_USER_CONFIGURATION = "User/UserConfiguration";
    public static readonly string PATH_USER_CONFIGURATION = Application.persistentDataPath+"/UserConfiguration.xml";
	#endregion
	
	#region FIELDS
    private UserConfiguration   userConfiguration;

    private static UserManager  instance = null;
	#endregion
	
	#region ACCESSORS
    public UserConfiguration UserConfiguration
    {
        get{ return userConfiguration; }
        set { UserConfiguration = value; }
    }

    public static UserManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UserManager();
            }

            return instance;
        }
    }
	#endregion
	
    #region METHODS_CONSTRUCTORS
    private UserManager() { }
    #endregion

	#region METHODS_CUSTOM
    public void Init(){
        if (!File.Exists(PATH_USER_CONFIGURATION)) {
            Debug.Log("No existe..." + PATH_USER_CONFIGURATION);
            string userConfFile = Resources.Load(PATH_RESOURCES_USER_CONFIGURATION).ToString();
            File.WriteAllText(PATH_USER_CONFIGURATION, userConfFile);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(UserConfiguration));
        FileStream stream = new FileStream(PATH_USER_CONFIGURATION, FileMode.OpenOrCreate);
        userConfiguration = (UserConfiguration)serializer.Deserialize(stream);

        stream.Close();
    }

    public void SaveUserConfiguration() {
        XmlSerializer serializer = new XmlSerializer(typeof(UserConfiguration));
        FileStream stream = new FileStream(PATH_USER_CONFIGURATION, FileMode.Create);
        serializer.Serialize(stream, userConfiguration);

        stream.Close();
    }
	#endregion
	
	#region METHODS_EVENT
	#endregion
}
