:: Install Graphviz, Create a environment variable DOX_GRAPHVIZ_PATH and set the installation bin folder

:: Download plantuml jar and locate on your computer and set a system enviroment variable (DOX_PLANTUML_JAR_PATH) for doxygen

:: for html help workshop
::use old downlaod url http://web.archive.org/web/20160201063255/http://download.microsoft.com/download/0/A/9/0A939EF6-E31C-430F-A3DF-DFAE7960D564/htmlhelp.exe

:: DOT_IMAGE_FORMAT = svg is used bot dot.exe and plantuml.jar image output formats configured in doxygen configuration file

:: DOT_PATH = "C:/Program Files (x86)/Graphviz/bin"
SET DOX_GRAPHVIZ_PATH="C:/Program Files (x86)/Graphviz/bin"

SET DOX_DOT_PATH="C:/Program Files (x86)/Graphviz/bin"

:: PLANTUML_JAR_PATH=plantuml/plantuml.jar
SET DOX_PLANTUML_JAR_PATH=plantuml/plantuml.jar

:: HHC_LOCATION = "C:/Program Files (x86)/HTML Help Workshop/hhc.exe"
SET DOX_HHC_LOCATION="C:/Program Files (x86)/HTML Help Workshop/hhc.exe"

doxygen doxyfile_dev

pause
