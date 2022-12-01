:: RTF: doxygen -w rtf styleSheetFile
doxygen -w rtf styleSheetFile.rtf
:: HTML: doxygen -w html headerFile footerFile styleSheetFile [configFile]
doxygen -w html headerFile.html footerFile.html styleSheetFile.css Doxyfile
:: LaTeX: doxygen -w latex headerFile footerFile styleSheetFile [configFile]
doxygen -w latex headerFile.latex footerFile.latex styleSheetFile.latex Doxyfile