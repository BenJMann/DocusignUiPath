######################################################
#                                                    #
#          Docusign Authentication Workflows         #
#                                                    #
######################################################


~~This file was automatically generated by the Docusign Authenticate Activity in UiPath~~


Modifying
---------
If you modify this file, the Authenticate activity will use the modified version.

Invoking Directly
-----------------
You can replace the Authenticate activity with an Invoke Workflow activity, pointing towards the generated file.
The following arguments are required:

	authUrl:	The authentication Url generated by the GetAuthorizationUrl Activity
	email:		The email login
	password:	The docusign password - either string or SecureString, depending on the authentication method
	timeout:	The amount of time the workflow waits for the initial page to be loaded

Resetting
---------
If you wish to reset any changes you've made back to the default, delete the generated file.
When you run the Authenticate Activity with the Authentication Method set to either "Secure" or "Insecure",
a new Workflow will be generated in this folder.


~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Copyright Ben Mann 2018

https://github.com/BenJMann/DocusignUiPath
License: https://github.com/BenJMann/DocusignUiPath/LICENSE.txt

~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~