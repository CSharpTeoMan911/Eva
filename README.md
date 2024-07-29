# Eva


<br>
<br>

## MAJOR PATCH

<br>

* Eva can now excute custom commands made by the user. Visit this [link](https://github.com/CSharpTeoMan911/Eva/wiki/Commands-customisation) for detailed instructions
* Eva can now become invisible. Say 'Listen', then say either 'visible' or 'invisible' to make the window invisible or visible accordingly.

<br>
<br>

## DOWNLOAD

<br>

Download Eva from SourceForge: https://sourceforge.net/projects/eva-ai/
<br>
Download Eva from Github: https://github.com/CSharpTeoMan911/Eva/releases/tag/Release

<br>
<br>

## DEVELOPER SUPPORT

<br>

For instructions about how to set up the environment for developent, how to add the resources needed by the application, and how to add the necessary libraries and SDK's : https://github.com/CSharpTeoMan911/Eva/wiki/Visual-Studio-configuration-and-operational-prerequisites

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

The Eva's core technologies are the Vosk In-proc speech recognition engine, the Microsoft online speech recognition engine, the .NET framework, Windows Presentation Foundation ( WPF ), and the Universal Windows Platforms ( UWP ). 

<br>

Vosk: https://github.com/alphacep/vosk-api

Speech recognition model: https://github.com/daanzu/kaldi-active-grammar/blob/master/docs/models.md

UWP Speech Recognition: https://learn.microsoft.com/en-us/uwp/api/windows.media.speechrecognition.speechrecognizer?view=winrt-22621

<br>
<br>

### Speech recognition infrastructure
_______________________________________

<br>


![Speech system flowchart](https://user-images.githubusercontent.com/87245086/234708319-0ad38208-afe3-460c-9066-224704151b20.png)


<br>

The Vosk speech recogniser is listening permanently, if the listening function is activated. Once it recognises the word "Listen" or "Hey listen", the online speech recognition engine is activated. The online speech recognition engine has the role of extracting commands and their content.

<br>
<br>

### Natural Language Understanding
____________________________________

<br>

Eva uses natural language processing in two ways, and these are speech recognition and contextual command and content extraction. Both, Vosk and Windows Online Speech Recognition engines use natural language processing in order to extract the words from any audio medium, and these features are built into both of the engines. 

<br>

The contextual command and content extraction natural language understanding engine is built into the application in order for Eva to understand the commands given by the user and their content, and this command and content extraction natural language understanding engine is built by me. The application's natural language understanding engine is doing this by following a set of procedures. Firstly, it is doing some tokenization in order to understand what process it has to execute and what extra parameters must be extracted from the input, in order to execute the command. This is done by analysing the input and by searching for some keywords that have to be at a certain index within the sentence. For example, if the command given is "open chrome", because the word ***open*** is the first word, the natural language understanding engine categorised the command as a process in which a certain application is opened. If the command given is "search leopard 1 tank blueprint on google", because the first word is search, the tokenisation is pointing to the parameter related to web search command. If the tokenization did not match the sentence with any parameter, then no process will be executed. 

<br>

A second tokenisation is performed, after the process type had been identified. When the process type had been identified, accordingly with the process type patern, if any secondary parameters are required by the process type, these will be extracted accordingly. For example, if the command "search black shoes of amazon" had been entered, the patern of this command is "search [ CONTENT ] on [ WEB APPLICATION ]". This means that the natural language understanding engine has to search for a web application and for the content to be searched on that web application. Then accordingly to the process patern, the natural language understanding engine knows that the content to be searched on the web application is between the words "search" and "on", and it knows that the web application keyword is every word after the word "on". If the second tokenization did not detect the correct variables format, then no process will be executed. 

<br>

A third tokenisation is performed in order to extract the variables content. This tokenisation is using the indexes of the variables detected in the second tokenisation, extracts the variables, and verifies the extracted variables against a list of valid variables. Once the variables are validated, the processes associated with the variables are extracted and set to be executed accordingly. If the third tokenization did not detect any valid values for the detected variables, then no process will be executed. 

<br>


![302436808-cbed89ab-0788-4fcf-976d-bb694cdb888f](https://github.com/CSharpTeoMan911/Eva/assets/87245086/bf68529e-295f-4300-9b8f-15959da55643)

<br>


#### Natural Language Understanding Engine Time Complexities
__________________________________________________________

<br>

![TIME COMPLEXITY](https://user-images.githubusercontent.com/87245086/234992408-7ec94b0f-4b13-483a-8182-2a00b332d6af.png)

<br>

The time complexity of the natural language understanding engine is in ***O(n)*** in the worst case and average case scenarios, and the time complexity ***O(n - ( n - (ci + 1) ))*** is in the best case scenario, where "ci" stands for current index where the engine could not match the input with any patern related to any process. Because the natural language understanding engine has 3 stages of tokenisation that are verifying if certain criterias are met within the given sentence, the natural language understanding engine will stop processing the information at the index where the sentence did not fulfil the specified criterias, depending at which tokenisation stage the natural language understanding engine the criterias are not satisfied, and thus the ***RAM*** memory resources and the ***CPU*** processing power are not wasted unnecessary.

<br>
<br>
<br>

# Gradient fluctuation formula
<br>

![Gradient fluctuation](https://user-images.githubusercontent.com/87245086/234971684-804bca17-440c-44d4-84e2-dd3a73a240d8.png)

<br>

<br>

![Linear representation of the gradient fluctuation](https://github.com/CSharpTeoMan911/Eva/assets/87245086/e3fcd77c-046d-49ba-97d0-dc8c22eac702)

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

In order to give commands to Eva say the word "Listen" or "Hey listen" followed by the desired command. For example, say "Hey listen", then after Eva is activated, say "search robots are cool on Google". You can find detailed instructions about commands and the command paterns and troubleshooting within the instruction manual of Eva.

<br>

![Eva_Capture2121712780](https://user-images.githubusercontent.com/87245086/201437406-45b9ef8a-5dc2-42d2-813b-9695436c46d7.jpg)

![Eva_Capture829367817](https://user-images.githubusercontent.com/87245086/201437415-e3107b6c-44f2-45d2-bbe5-bcccd6f25ca6.jpg)

![Eva_Capture1195645157](https://user-images.githubusercontent.com/87245086/201437423-54a0cc92-6473-4675-94ee-71b3fd7d9ea0.jpg)

![Eva_Capture1277613527](https://user-images.githubusercontent.com/87245086/201437431-b0853d46-8418-49cc-bb01-1714ed4064f0.jpg)

![Eva_Capture34673362](https://user-images.githubusercontent.com/87245086/201437439-a3196b97-7d39-457e-ac38-38300363ab61.jpg)

<br>

# Command customisation

![Eva_Capture2072297579](https://github.com/CSharpTeoMan911/Eva/assets/87245086/6076ec7e-f28c-46fc-8ed2-1f2cfc1e6ff8)

Add, remove, and modify commands as you desire. For detailed instruction regarding command customisation, go to the Wiki page https://github.com/CSharpTeoMan911/Eva/wiki/Commands-customisation 

<br>

# What Eva can do?

<br>

## It can give commands to ChatGPT

<br>

![Eva_Capture859377083](https://user-images.githubusercontent.com/87245086/234132245-9aa81de9-5f31-47da-94e0-480917bf8995.jpg)

![Eva_Capture1166755094](https://user-images.githubusercontent.com/87245086/234132257-9282765f-ff1a-4ae9-8ecc-edb0c9f14ccf.jpg)

![Eva_Capture443117994](https://user-images.githubusercontent.com/87245086/234130364-07979c73-ecd3-42a1-a69d-6a65591a4133.jpg)

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

## It can search on Reddit

<br>

![Eva_Capture1857061334](https://user-images.githubusercontent.com/87245086/229218589-cbac7e37-3c14-4541-a6f5-660dc78e69ff.jpg)

<br>

## It can search on Facebook

<br>

![Eva_Capture719492630](https://user-images.githubusercontent.com/87245086/229218831-b215de4b-aa78-4b70-be10-d08f6ddba158.jpg)

<br>

## It can search on Instagram

<br>

![Eva_Capture934103573](https://user-images.githubusercontent.com/87245086/229219394-d3dc45a2-4da9-48a8-b609-87ed6c41cdfa.jpg)

<br>

## It can search on Gmail

<br>

![Eva_Capture2104650543](https://user-images.githubusercontent.com/87245086/229219627-59f734c2-4deb-44a7-8fc5-1d86eb4ef7db.jpg)

<br>

## It can search on Twitter

<br>

![Eva_Capture144044807](https://user-images.githubusercontent.com/87245086/229219803-dcf7c33c-5280-4fcb-aac9-9407d5b82be0.jpg)

<br>

## It can search on Pinterest

<br>

![Eva_Capture571896073](https://user-images.githubusercontent.com/87245086/229220011-8f3bf0f7-2832-44bc-99a2-870d770b01ef.jpg)

<br>

## It can search on LinkedIn

<br>

![Eva_Capture1342419452](https://user-images.githubusercontent.com/87245086/229220221-41099e9e-c27c-41d1-ba91-42376061a9c8.jpg)

<br>

## It can search on Github

<br>

![Eva_Capture1030181943](https://user-images.githubusercontent.com/87245086/229220365-81212deb-2b29-448a-a748-59dc7e7b6f27.jpg)

<br>

## It can search on Unsplash

<br>

![Eva_Capture1898758186](https://user-images.githubusercontent.com/87245086/229220460-a68e8f64-e6fa-4b87-be51-b0eef14efc01.jpg)

<br>

## It can search on Stackoverflow

<br>

![Eva_Capture1340621546](https://user-images.githubusercontent.com/87245086/229220632-ef4ddd23-b567-4179-96e6-ace092304d60.jpg)

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

# Extra information

<br>

Eva is open source and free to use. You can find extra informations on the [**Wiki**](https://github.com/CSharpTeoMan911/Eva/wiki) section of the repository. 

<br>

![Eva_Capture2116139740](https://user-images.githubusercontent.com/87245086/201439583-33776568-07bb-4217-842b-1506c87b6428.jpg)

![Eva_Capture1481952206](https://user-images.githubusercontent.com/87245086/201439590-21d8f883-b1db-4dac-87da-a5839317fa07.jpg)

<br>
<br>
</html>



