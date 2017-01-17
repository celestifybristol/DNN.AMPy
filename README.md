DNN AMP Conversion Module
================================

When placed on a page, this crude module will create a static HTML file of the modules within the main content pane. So far only tested with standard HTML modules.

This module is currently built as a WSP for the sake of initial development speed. It will be converted to a WAP in the future. 

TODO
=============
1. Finish removing wrapping divs for content. div class="DnnModule" class="DNNContainer_noTitle"
2. Remove empty div class="clear", remove module links a name="880"
3. Auto size & height images based on style tags //DONE
4. Add AMP nav, logo //DONE
5. CSS styling - Create an AMP CSS file. Place in portal root?? Read file and import into doc

6. Module Settings
 - URL override
 - Module chooser
 - Other AMP features per module maybe (https://ampbyexample.com/#components)
   - Accordion
   - light box
   - iframe
   - analytics

7. Portal Settings
 - CSS
 - templating
 - amp font
 - menu creator

8. Build settings into DNN 9 toolbar


9. CSS ignore 
 - if a module has a class or container with "skip-amp" then it should be ignored.
