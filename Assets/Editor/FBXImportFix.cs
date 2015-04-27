using UnityEngine;
using UnityEditor;

public class FBXImportFix : AssetPostprocessor {

	void OnPreprocessModel() {

	}

	void OnPostprocessModel(GameObject gameObject) {
		//if file scale is 'meters' set the transform scale to 1,1,1
		//the defailt is a scale of 0.01 (y._.)
		if ((assetImporter as ModelImporter).fileScale == 1) {
			gameObject.transform.localScale = new Vector3 (1, 1, 1);
		}
	}

}
