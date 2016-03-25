<?xml version="1.0"  ?>

<!-- This XSL file transforms a Mindmap XML file from MindManager X5
     to a LaTeX file. 

     It was written to structure and write a LaTeX document comletely
     in MindManager including text (which is written in the notes). At
     the end, the Mindmap can be exported and run trogh LaTeX to get
     the final document.

     FUNCTIONALITY

       The transformation only exports those topics in a mindmap which
       have an associated note. The topic text is then converted as a
       \section{...}, the note as a paragraph. Some markups in the
       note such as bold face, or bulleted lists are properly
       converted to LaTeX code.

       Topics in the map, which do not have a note are not exported
       unless there are descendant topics, which do have a note.

       There is one convenient hack: Sometimes, I like to convert
       subtopics to a bulleted list where the first line of each item
       is the topic title. For this purpose, one has to indicate these
       notes by a boundary around them, see example file.

     PROBLEMS

       * Sorry about the style, this is my first XSL transformation

       * This XSL file can currently be only transformed using saxon,
         tested on saxonb 8.5, http://www.saxonica.com/
    
     TODO
  
       * conversion to a proper date format

       * conversion of special charactars
     
       * referencing
      
       * conversion of binary data would be cool

     V 0.01 
     Stephan Heuel 12.08.2005

     V 0.01a
     changed some comments


     some parts have been adapted from xh2latex.xsl 
     by Eitan M. Gurari July 19, 2000
-->

<xsl:stylesheet version="1.0" 
		xmlns:ap="http://schemas.mindjet.com/MindManager/Application/2003" 
		xmlns:cor="http://schemas.mindjet.com/MindManager/Core/2003" 
		xmlns:pri="http://schemas.mindjet.com/MindManager/Primitive/2003"
		xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
		xmlns:xhtml="http://www.w3.org/1999/xhtml"
		xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="text" indent="no"/>
  
  <xsl:template match="/">
    <xsl:text>
%% 
%% This document was automatically generated from a Mindmap (MindManager X5)
%%
\documentclass{article}
    </xsl:text>    
    <xsl:apply-templates select="ap:Map" mode="preamble"/>
    <xsl:text>
\begin{document} 
\maketitle
\tableofcontents
\newpage
    </xsl:text>
    <xsl:apply-templates select="ap:Map" mode="document"/>
    <xsl:text>
\end{document} 
    </xsl:text>    
  </xsl:template>


  <!--   Preamble Processing -->
  <!--   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
  <xsl:template match="cor:Uri" mode="preamble"/> <!-- don't match binary elements -->
  <xsl:template match="cor:Base64" mode="preamble"/> <!-- don't match binary elements -->

  <xsl:template match="ap:DocumentGroup/ap:Author" mode="preamble">
    <xsl:text>
\author{</xsl:text>
    <xsl:value-of select="@UserName"/>
    <xsl:text>}
    </xsl:text>
  </xsl:template>
  
  <xsl:template match="ap:DocumentGroup/ap:DateTimeStamps" mode="preamble">
    <xsl:text>
\date{</xsl:text>
    <xsl:value-of select="@LastModified"/>
    <xsl:text>}
    </xsl:text>    
  </xsl:template>
  
  <xsl:template match="ap:OneTopic/ap:Topic" mode="preamble">
    <xsl:text>
\title{</xsl:text>
    <xsl:value-of select="ap:Text/@PlainText"/>
    <xsl:text>}
    </xsl:text>
  </xsl:template>
  <xsl:template match="xhtml:*" mode="preamble"/>
  <xsl:template match="xhtml:*"/>

 
  <!--   Document Processing -->
  <!--   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
  
  <!-- don't match binary elements (yet) -->
  <xsl:template match="cor:Uri" mode="document"/> 
  <xsl:template match="cor:Base64" mode="document"/> 
  
  <!-- This is the start -->
  <xsl:template match="ap:OneTopic/ap:Topic" mode="document">
    <xsl:apply-templates select="ap:SubTopics/ap:Topic" mode="document"/>
  </xsl:template>

  <!-- Sections Processing -->
  <xsl:template match="ap:SubTopics/ap:Topic" mode="document">
    <xsl:choose>
      <!-- Here we check if this is the beginning of a boundary,
	   i.e. that we deal with items rather than sections for the
	   subsequent topic hierarchy-->
      <xsl:when test="ap:OneBoundary">
	<!-- So this node is the first within the boundary, we have to
	     check, if the upper node had a boundary to decide whether
	     this one is an item or a section -->
	<xsl:choose>
	  <xsl:when test="../../ap:OneBoundary"><xsl:call-template name="ItemizeGenerator"/></xsl:when>
	  <xsl:otherwise><xsl:call-template name="SectionGenerator"/></xsl:otherwise>
	</xsl:choose>
	<xsl:text>\begin{itemize}
</xsl:text>	    
      </xsl:when>
      
      <!-- if we are inside a boundary, but not at the beginning, then
	     topics are items in an itemize environment -->
      <xsl:when test="ancestor::*/ap:OneBoundary">
	<xsl:call-template name="ItemizeGenerator"/>
      </xsl:when>

      <!-- otherwise topics are sections -->
      <xsl:otherwise>
	<xsl:if test="descendant-or-self::*/ap:NotesGroup">
	  <xsl:call-template name="SectionGenerator"/>
	</xsl:if>
      </xsl:otherwise>
    </xsl:choose>

    <xsl:apply-templates select="ap:SubTopics/ap:Topic" mode="document"/>
    
    <xsl:if  test="ap:OneBoundary">
      <xsl:text>\end{itemize}
      </xsl:text>	    
    </xsl:if>

  </xsl:template>
  <xsl:template match="xhtml:*" mode="document"/>
  <!-- End of Sections Processing -->
  
  <!-- This template generates an \item for a topic and output the
       note as paragraph -->
  <xsl:template name="ItemizeGenerator">
    <xsl:if test="../../ap:OneBoundary">
      <xsl:text>\item </xsl:text>
      <xsl:value-of select="ap:Text/@PlainText"/>
      <xsl:text>

</xsl:text>
    </xsl:if>
    <xsl:apply-templates select="ap:NotesGroup/ap:NotesXhtmlData" mode="document"/>
  </xsl:template>

  <!-- This template generates a \section{} for a topic and output the
       note as a paragraph -->
  <xsl:template name="SectionGenerator">
      <xsl:text>
\</xsl:text>
      <xsl:if test="((count(ancestor::node())-3) div 2)>1">
	<xsl:call-template name="SubGenerator">
    	  <xsl:with-param name="count" select="(count(ancestor::node())-3) div 2"/>
	</xsl:call-template>
      </xsl:if>
      <xsl:text>section{</xsl:text>
      <xsl:value-of select="ap:Text/@PlainText"/><xsl:text>}
      </xsl:text>
      <xsl:apply-templates select="ap:NotesGroup/ap:NotesXhtmlData" mode="document"/>
  </xsl:template>
  
  <!-- SubGenerator: A recursive function that generates "sub" strings-->
  <xsl:template name="SubGenerator">
    <xsl:param name="count"/>
    <xsl:if test="$count != 1">
      <xsl:text>sub</xsl:text>
      <xsl:call-template name="SubGenerator">
	<xsl:with-param name="count" select="$count - 1"/> 
	<!-- 2 because it's organized like this Section/Body/Section ... etc. -->
      </xsl:call-template>
    </xsl:if>
  </xsl:template>
  <!-- End of SubGenerator-->

  <!--   Note Processing -->
  <!--   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
  <xsl:template match="ap:NotesGroup/ap:NotesXhtmlData"
		mode="document">
    <xsl:apply-templates select="xhtml:*" mode="note"/> 
  </xsl:template>


  <xsl:template match="xhtml:i" mode="note">
    <xsl:text>\textit{</xsl:text>
    <xsl:apply-templates mode="note"/>
    <xsl:text>}</xsl:text>
  </xsl:template>

  <xsl:template match="xhtml:b" mode="note">
    <xsl:text>\textbf{</xsl:text>
    <xsl:apply-templates mode="note"/>
    <xsl:text>}</xsl:text>
  </xsl:template>

  <xsl:template match="xhtml:img" mode="note">
    <xsl:text>%% include image</xsl:text>
  </xsl:template>

  <xsl:template match="xhtml:br" mode="note">
    <xsl:text>

</xsl:text>
  </xsl:template>

  <xsl:template match="xhtml:span" mode="note">
    <xsl:apply-templates mode="note"/>
  </xsl:template>
  <xsl:template match="xhtml:p" mode="note">
    <xsl:text>

</xsl:text>
    <xsl:apply-templates mode="note"/>
  </xsl:template>

  <!-- lists -->
  <xsl:template match="xhtml:ul" mode="note">
    <xsl:text>\begin{itemize}
</xsl:text>
    <xsl:for-each select="xhtml:li">
      <xsl:text>
	\item </xsl:text>
      <xsl:apply-templates mode="note"/>
    </xsl:for-each>
    <xsl:text>
\end{itemize}
    </xsl:text>
  </xsl:template>

  <!-- End of Notes processing -->

  <!-- TODO text (has to be paresed for special characters, etc. ) -->

</xsl:stylesheet>
