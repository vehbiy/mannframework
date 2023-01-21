<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<!--
SQL Data Compare
SQL Data Compare
Version:11.1.3.23-->
<Project version="3" type="SQLComparisonToolsProject">
  <DataSource1 version="3" type="LiveDatabaseSource">
    <ServerName>SOLARISMAC</ServerName>
    <DatabaseName>buyback</DatabaseName>
    <Username />
    <SavePassword>False</SavePassword>
    <Password />
    <ScriptFolderLocation />
    <MigrationsFolderLocation />
    <IntegratedSecurity>True</IntegratedSecurity>
  </DataSource1>
  <DataSource2 version="3" type="LiveDatabaseSource">
    <ServerName>garciadb.database.windows.net</ServerName>
    <DatabaseName>Buyback</DatabaseName>
    <Username>garciadb</Username>
    <SavePassword>True</SavePassword>
    <Password encrypted="1">fILyjQJOlsCdgB3ct/gd+A==</Password>
    <ScriptFolderLocation />
    <MigrationsFolderLocation />
    <IntegratedSecurity>False</IntegratedSecurity>
  </DataSource2>
  <LastCompared>06/14/2019 13:10:03</LastCompared>
  <Options>Default</Options>
  <InRecycleBin>False</InRecycleBin>
  <Direction>1</Direction>
  <ProjectFilter version="1" type="DifferenceFilter">
    <FilterCaseSensitive>False</FilterCaseSensitive>
    <Filters version="1">
      <None version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </None>
      <Assembly version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Assembly>
      <AsymmetricKey version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </AsymmetricKey>
      <Certificate version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Certificate>
      <Contract version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Contract>
      <DdlTrigger version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </DdlTrigger>
      <Default version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Default>
      <ExtendedProperty version="1">
        <Include>True</Include>
        <Expression />
      </ExtendedProperty>
      <EventNotification version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </EventNotification>
      <FullTextCatalog version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </FullTextCatalog>
      <FullTextStoplist version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </FullTextStoplist>
      <Function version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Function>
      <MessageType version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </MessageType>
      <PartitionFunction version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </PartitionFunction>
      <PartitionScheme version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </PartitionScheme>
      <Queue version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Queue>
      <Role version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Role>
      <Route version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Route>
      <Rule version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Rule>
      <Schema version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Schema>
      <SearchPropertyList version="1">
        <Include>True</Include>
        <Expression />
      </SearchPropertyList>
      <Sequence version="1">
        <Include>True</Include>
        <Expression />
      </Sequence>
      <Service version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Service>
      <ServiceBinding version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </ServiceBinding>
      <StoredProcedure version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </StoredProcedure>
      <SymmetricKey version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </SymmetricKey>
      <Synonym version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Synonym>
      <Table version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Table>
      <User version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </User>
      <UserDefinedType version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </UserDefinedType>
      <View version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </View>
      <XmlSchemaCollection version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </XmlSchemaCollection>
    </Filters>
  </ProjectFilter>
  <ProjectFilterName />
  <UserNote />
  <SelectedSyncObjects version="1" type="SelectedSyncObjects">
    <Schemas type="ListString" version="2" />
    <Grouping type="ListByte" version="2">
      <value type="Byte">0</value>
      <value type="Byte">0</value>
      <value type="Byte">0</value>
      <value type="Byte">0</value>
      <value type="Byte">0</value>
    </Grouping>
    <SelectAll>False</SelectAll>
  </SelectedSyncObjects>
  <SCGroupingStyle>0</SCGroupingStyle>
  <SQLOptions>10</SQLOptions>
  <MappingOptions>82</MappingOptions>
  <ComparisonOptions>0</ComparisonOptions>
  <TableActions type="ArrayList" version="1">
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[City]:[dbo].[City]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[Country]:[dbo].[Country]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[Language]:[dbo].[Language]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[ItemProperty]:[dbo].[ItemProperty]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[Item]:[dbo].[Item]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[LocalizationItem]:[dbo].[LocalizationItem]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[MemberProject]:[dbo].[MemberProject]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[Project]:[dbo].[Project]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[ProjectGroup]:[dbo].[ProjectGroup]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[ProjectMapping]:[dbo].[ProjectMapping]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[ProjectSetting]:[dbo].[ProjectSetting]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>DeselectItem</action>
      <val>[dbo].[SubProject]:[dbo].[SubProject]</val>
    </value>
  </TableActions>
  <SessionSettings>15</SessionSettings>
  <DCGroupingStyle>0</DCGroupingStyle>
  <SC_DeploymentOptions version="1" type="SC_DeploymentOptions">
    <BackupOptions version="1" type="BackupOptions">
      <BackupProvider>Native</BackupProvider>
      <TypeOfBackup>Full</TypeOfBackup>
      <Folder>C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup</Folder>
      <Filename />
      <SqbLicenseType>None</SqbLicenseType>
      <SqbVersion>0</SqbVersion>
      <DefaultNativeFolder>C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup</DefaultNativeFolder>
      <DefaultSqbFolder />
      <Password encrypted="1" />
      <NameFileAutomatically>False</NameFileAutomatically>
      <OverwriteIfExists>False</OverwriteIfExists>
      <CompressionLevel>0</CompressionLevel>
      <EncryptionLevel>None</EncryptionLevel>
      <ThreadCount>0</ThreadCount>
      <BackupEnabled>False</BackupEnabled>
    </BackupOptions>
  </SC_DeploymentOptions>
</Project>