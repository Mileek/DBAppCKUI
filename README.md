<div id="top"></div>




<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

<p align="center">
  <a href="https://www.linkedin.com/in/kamil-p-kaszuba/"><img src="https://img.shields.io/badge/Linkedin-302f2c?style=for-the-badge&logo=Linkedin&logoColor=blue" alt="Linkedin">
  </a>
</p>

<br/>
<br/>
<br/>




<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Mileek/ReSCat">
    <img src="https://user-images.githubusercontent.com/95537833/152524739-901694ee-dbdb-491f-89d0-742c1a66bada.png" alt="Logo" width="140" height="140">
  </a>

  <h3 align="center">ReSCat</h3>

  <p align="center">
    Company report creating application.
    <br />
    <!--
    <a href="https://github.com/othneildrew/Best-README-Template"><strong>Explore the docs »</strong></a>
    <br />
    -->
    <br />
    <a href="https://github.com/Mileek/ReSCat/blob/master/ReSCat/View/ReSCatMainWindow.xaml.cs">Main Window Code</a>
    ·
    <a href="https://github.com/Mileek/ReSCat/blob/master/ReSCat/View/ReSCatMainWindow.xaml">Main Window View</a>
    ·
    <a href="https://github.com/Mileek/ReSCat/tree/master/ReSCat/Resources">App Themes</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

![image](https://user-images.githubusercontent.com/95537833/152528784-5e2798b1-72f6-468f-9ac7-e4b129af4f5a.png)


What you are seeing is a simple wpf application, in which i used to learn: WPF, C#, XAML, SQL and Entity Framework and ClosedXML. Everything written inside is based on my own insights, tips from stackoverflow or diffrent pages.

The application is based on the work i did in my last company. Basing on main entity that is pattern, you can create whole database with elements that are manufactured in company. There are buttons responsible to: add, delete, update, show (which are used to write/modify records) new records into db. Datagrid is used to display all records inside db (based on entity using SQL). On the left side there is a possibility of searching specific records from database (or loaded XML file). Program also have functionality that allows you to create XML file from your current records. You can also load previously created XML file, edit, add, update and save your changes so that you don't need to use another program to work with it.

There are also functions like:

* Calendar which shows selected week (for production purposes),

* In-Build calculator (it is possible to choose Windows calculator),

* Simple notepad,

* Nawigation menu.



<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

Frameworks used in the project:

* [Entity Framework](https://docs.microsoft.com/en-us/ef/)
* [ClosedXML](https://github.com/ClosedXML/ClosedXML/)



<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

Application is still in development and many of its functionalities are not bug-proof.


### Prerequisites

You need to have SQL Server Express LocalDB installed due to .mdf file with main data base info.


### Installation

To run the application all you have to do is download pre-relese version and then install it.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Application was created to:
* Create empty database - In `Menu` after pressing `New`, then you have to choose the name of your DB.
* Edit existing xml - In `Menu` you could choose to edit existing XML raport to add missing data.
* Saving database as XML - In `Menu` you could save the file to create XML file as a report.
* Modifying data - It is the main feature of the application. It is possible to add, delete and update records in database. There is also an option to check the elements as finished products. All recors may be filtered by finished products.
* Searching through the data - there is also a special panel to search the data by selecting appropriate header.
* Hall box - Allows you to select on which hall the element is manufactured.
* Special buttons on right panel - There are also buttons responsible for: `Notes` that could be saved in `.txt` file. Possibility to switch between in-build `Calcultator` and system `Calculator`. `Calendar` that is probably most helpful function, it show you the week selected day to write it into the appropriate boxes.

The application was meant to be self-sufficient. In my previous company I had to use many different XML files to add new record, create new rows (It's not so efficient!), use browser to check production week of th element, calculator to count the weight, notepad to create simple notes. As you could probably imagine it was not so fast, and not so plesant to work with it. You probably also know that after sharing an XML file, someone else might open it (So you can't edit it) then forget about it and so your work stops.



<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [x] Add Week Calendar
- [x] Add Nawigation Panel
- [x] XML Files Handling 
- [ ] Bug, expection handling
- [ ] Add New Panels (Charts, Logs)
- [ ] Add functional minimalize and close buttons
- [ ] Implementing MVVM model
- [ ] Add Login Menu
- [ ] Admin Functionalities
- [ ] Notepad And Calculator full functionality




<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

feel free to add something to my project!


1. Fork the Project
2. Create your Feature Branch 
3. Commit your Changes 
4. Push to the Branch 
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License (so basicilly you coul do whatever you want with my code). 

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Kamil Kaszuba - [Linkedin](https://www.linkedin.com/in/kamil-p-kaszuba/) - kamil.p.kaszuba@gmail.com

Project Link: [https://github.com/Mileek/ReSCat](https://github.com/Mileek/ReSCat)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

Use this space to list resources you find helpful and would like to give credit to. I've included a few of my favorites to kick things off!

* [Tim Corey](https://www.iamtimcorey.com)
* [Stack Overflow](https://stackoverflow.com)
* [Entity Framework Tutorial](https://www.entityframeworktutorial.net)
* [ReadME Template](https://github.com/othneildrew/Best-README-Template)




<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/othneildrew/Best-README-Template/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/othneildrew/Best-README-Template.svg?style=for-the-badge
[forks-url]: https://github.com/othneildrew/Best-README-Template/network/members
[stars-shield]: https://img.shields.io/github/stars/othneildrew/Best-README-Template.svg?style=for-the-badge
[stars-url]: https://github.com/othneildrew/Best-README-Template/stargazers
[issues-shield]: https://img.shields.io/github/issues/othneildrew/Best-README-Template.svg?style=for-the-badge
[issues-url]: https://github.com/othneildrew/Best-README-Template/issues
[license-shield]: https://img.shields.io/github/license/othneildrew/Best-README-Template.svg?style=for-the-badge
[license-url]: https://github.com/othneildrew/Best-README-Template/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/kamil-p-kaszuba/
[product-screenshot]: images/screenshot.png
