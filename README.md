# Eva


<br>
<br>

## MAJOR PATCH

<br>

Changed the wake word engine from Windows Speech Recognition (SAPI) to PocketSphinx

<br>
<br>

## DOWNLOAD

<br>

Download Eva: https://drive.google.com/file/d/14hdzQuxMqruI7ISovDoWDBPY_x0mNBw2/view?usp=sharing

<br>
<br>

## DEVELOPER SUPPORT

<br>

Click this link to see how to set up the environment for developent: https://github.com/CSharpTeoMan911/Eva/wiki/Visual-Studio-configuration-and-operational-prerequisites

<br>
<br>
<br>
<br>

## About

<br> 

Eva is an A.I. assistant that has the purpose of helping users multi-task. It also has the use of helping people with mental and phisical disabilities. All the commands to be executed are given to Eva through voice commmands.

<br> 

## Technologies

<br>

The Eva's core technologies are the PocketSphinx In-proc speech recognition engine, the Microsoft online speech recognition engine, the Dot NET framework, Windows Presentation Foundation ( WPF ), and the Universal Windows Platforms ( UWP ). 

<br>

### Speech recognition infrastructure
_______________________________________

<br>


![Wake Word Mechanism](https://user-images.githubusercontent.com/87245086/213821144-680842ce-ad66-432f-ba6e-5595b2122650.png)


<br>

The PocketSphinx speech recogniser is listening permanently, if the listening function is activated. Once it recognises the word "Eva", the online speech recognition engine is activated. The online speech recognition engine has the role of extracting the commands and their content.

<br>
<br>

### Natural Language Processing
____________________________________

<br>

Eva uses natural language processing in two ways, and these are speech recognition and contextual command and content extraction. Both the PocketSphinx and the Online Speech Recognition engines use natural language processing in order to extract the words from any audio medium, and these are built into both of the engines. 

<br>

The contextual command and content extraction natural language processing engine is built into the application in order for Eva to understand the commands given by the user and their content, and this command and content extraction natural language processing engine is built by me. The application's natural language processing engine is doing this by following some steps. First it is doing some tokenization in order to understand what process it has to execute and what extra parameters must be extracted from the input, in order to satify the process. This is done by analysing the input and by searching for some keywords that have to be at a certain index within the sentence. For example, if the command given is "open chrome", because the word open is the first word, the natural language processing engine categorised the command as a process in which a certain application is opened. If the command given is "search leopard 1 tank blueprint on google", because the first word is search, the tokenisation is pointing to the parameter related to web search. If the tokenization did not match the sentence with any parameter, than no process will be executed. 

<br>

A second tokenisation is performed, after the process type had been identified. When the process type had been identified, accordingly with the process type patern, if any secondary parameters are required by the process type, these will be extracted accordingly. For example, if the command "search black shoes of amazon" had been entered, the patern of this command is "search [ CONTENT ] on [ WEB APPLICATION ]". This means that the natural language processing engine has to search for a web application and for the content to be searched on that web application. Then accordingly to the process patern, the natural language processing engine knows that the web application keyword is between the words "search" and "on", and it knows that the content to be searched on the web application is every word after the word "on".

<br>


![Eva current definitive NLP Model](https://user-images.githubusercontent.com/87245086/201433713-d06bc37a-69a2-4f7c-b8f6-838bd5bce1f0.png)


<br>

#### Natural Language Processing Engine Time Complexities
__________________________________________________________

<br>

![Natural Language Processing Time Complexities](https://user-images.githubusercontent.com/87245086/201435692-28cf26df-b884-42c2-9f88-e07ab35d011b.png)

<br>

The time complexity of the natural language processing engine is in O(n) in the worst case scenario and O(n - ( n - ci )) in the best case scenario, where "ci" stands for current index where the engine could not match the input with any patern related to any process.

<br>
<br>
<br>

# Gradient fluctuation formula
<br>

![Gradient Fluctuation Formula For Animations](https://user-images.githubusercontent.com/87245086/205466011-f7030d0c-1ac5-4cce-b969-21ff8cd95c0c.png)

<br>

Eva uses an algorithm developed by me in order for the application to have those beautiful graphical user interface effects. This algorithm functions by increasing the offset of the gradient by using a sumation that is incrementing the value of the gradient until it reaches a threshold value. When the threshold value is reached, a sumation is used to decrement the value of the gradient until it reaches its original value. This gradient fluctuation formula can also be interpreted as two linear functions that are instantiated when each function's threshold value has to be reached. The aforementioned functions are y = x + v and y = x - v respectively, where y is the resulting gradient value, x is the current gradient value, and v is the value to be added or substracted from the current gradient value, and where x, y, and v
are greater than zero.
<br>
<br>
<br>


# Installation

<br>

Go to the ***DOWNLOAD*** section within the Eva's README.md page and click on the link.

<br>

![Eva_Capture733714694](https://user-images.githubusercontent.com/87245086/213866622-d3982009-8c14-4772-8958-a12f979806fd.jpg)


<br>
<br>


# Usage

<br>

In order to give commands to Eva say the word "Eva" followed by the desired command. For example, say "Eva" than after Eva is activated say "search robots are cool on Google". You can find detailed instructions about commands and the command paterns and troubleshooting within the instruction manual of Eva.

<br>

![Eva_Capture2121712780](https://user-images.githubusercontent.com/87245086/201437406-45b9ef8a-5dc2-42d2-813b-9695436c46d7.jpg)

![Eva_Capture829367817](https://user-images.githubusercontent.com/87245086/201437415-e3107b6c-44f2-45d2-bbe5-bcccd6f25ca6.jpg)

![Eva_Capture1195645157](https://user-images.githubusercontent.com/87245086/201437423-54a0cc92-6473-4675-94ee-71b3fd7d9ea0.jpg)

![Eva_Capture1277613527](https://user-images.githubusercontent.com/87245086/201437431-b0853d46-8418-49cc-bb01-1714ed4064f0.jpg)

![Eva_Capture34673362](https://user-images.githubusercontent.com/87245086/201437439-a3196b97-7d39-457e-ac38-38300363ab61.jpg)

<br>

# What Eva can do?

<br>

## It can set a timer

<br>

![Eva_Capture503136236](https://user-images.githubusercontent.com/87245086/201438495-856d831c-6ea7-432e-a510-fbcb47de5b86.jpg)

![Eva_Capture885556763](https://user-images.githubusercontent.com/87245086/201438506-20d08227-9722-4812-8b43-3a79e1fbd3e8.jpg)

<br>

## It can take screenshots

<br>

![Eva_Capture2037081470](https://user-images.githubusercontent.com/87245086/201479017-97b49edf-d54c-46b9-87b0-c0feedac00a9.jpg)

<br>

## It can search on Google

<br>

![Eva_Capture2116847400](https://user-images.githubusercontent.com/87245086/201438605-8b1d54d3-4208-4a8d-88fa-23c1ab413477.jpg)

<br>

## It can search on Google News

<br>

![Eva_Capture1332318160](https://user-images.githubusercontent.com/87245086/201438689-1bfeb4fa-5d98-4953-a5ac-351152ec5724.jpg)

<br>

## It can search on Google Images

<br>

![Eva_Capture1895286925](https://user-images.githubusercontent.com/87245086/201438735-185cc24d-ec54-4bc3-adfa-6fcf71063999.jpg)

<br>

## It can search on eBay

<br>

![Eva_Capture1343975081](https://user-images.githubusercontent.com/87245086/201438775-0ff79a5b-dcd4-41c4-b972-294836c99b29.jpg)

<br>

## It can search on Amazon

<br>

![Eva_Capture759505394](https://user-images.githubusercontent.com/87245086/201438812-776be9a4-1aea-45f1-b27d-b6dc1150f32d.jpg)

<br>

## It can search on Wikipedia

<br>

![Eva_Capture903369397](https://user-images.githubusercontent.com/87245086/201438847-d34a1894-46f6-41be-a848-c9d3f6b05e3a.jpg)

<br>

## It can search on Netflix

<br>

![Eva_Capture2144580495](https://user-images.githubusercontent.com/87245086/201479457-2f1c0609-9c41-4481-8142-30f6db61528a.jpg)

<br>

## It can open and close the operating system's settings

<br>

![Eva_Capture1994252519](https://user-images.githubusercontent.com/87245086/201438947-8bac14cd-794b-42b2-b443-930479ef2cbf.jpg)

<br>

## It can open and close multiple applications, such as chrome, firefox, notepad, chrome, visual studio code . . .

<br>

![Eva_Capture204556584](https://user-images.githubusercontent.com/87245086/201439146-1a57e248-f545-4c3e-9145-dae08eca19aa.jpg)

![Eva_Capture24348866](https://user-images.githubusercontent.com/87245086/201439151-fb4192a6-6f61-4aba-9b8a-08fc398fbf94.jpg)

<br>
<br>
<br>

# Extra informations

<br>

Eva is open source and free to use. You can find extra informations on the [**Wiki**](https://github.com/CSharpTeoMan911/Eva/wiki) section of the repository. 

<br>

![Eva_Capture2116139740](https://user-images.githubusercontent.com/87245086/201439583-33776568-07bb-4217-842b-1506c87b6428.jpg)

![Eva_Capture1481952206](https://user-images.githubusercontent.com/87245086/201439590-21d8f883-b1db-4dac-87da-a5839317fa07.jpg)

<br>
<br>
<br>

<html>

# Donations

<head>



</head>

<body>

### <div > <p>Please donate any sum of money in order to <br> buy resources and certifications, in order to <br> maintain current projects like Eva or create <br> new projects. Any amount is welcomed, <br> thank you.  </p> </div>




<br>

<div > <p> 

&nbsp; 

![homeless-man-eats-soup-from-plate-near-ruins-helping-poor-hungry-people-during-epidemic_89718-689](https://user-images.githubusercontent.com/87245086/201202844-c935b319-239d-4992-a2d4-ac225d51e3f3.jpg)







</p> </div>


<br>
<br>
<br>


&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?hosted_button_id=V5H8D2XRGRPHU)

</body>

</html>



