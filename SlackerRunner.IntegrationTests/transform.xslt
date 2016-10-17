<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="xml" indent="yes" />
  <xsl:template match="/assemblies">
    <onetotal>
      <xsl:value-of select="sum(//assembly/collection/@total)" />
    </onetotal>
  </xsl:template>
</xsl:stylesheet>