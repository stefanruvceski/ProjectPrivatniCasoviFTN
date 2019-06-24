<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="PrivatniCasoviFTN" generation="1" functional="0" release="0" Id="f393b7bf-2150-4a2d-95ea-1b99843d277e" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="PrivatniCasoviFTNGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="PrivatniCasoviAPI:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/LB:PrivatniCasoviAPI:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="PrivatniCasoviAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapPrivatniCasoviAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="PrivatniCasoviAPIInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapPrivatniCasoviAPIInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:PrivatniCasoviAPI:Endpoint1">
          <toPorts>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPI/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapPrivatniCasoviAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPI/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapPrivatniCasoviAPIInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPIInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="PrivatniCasoviAPI" generation="1" functional="0" release="0" software="C:\Users\Stefan\Documents\GitHub\ProjectPrivatniCasoviFTN\PrivatniCasoviFTN\PrivatniCasoviFTN\csx\Debug\roles\PrivatniCasoviAPI" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;PrivatniCasoviAPI&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;PrivatniCasoviAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPIInstances" />
            <sCSPolicyUpdateDomainMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPIUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPIFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="PrivatniCasoviAPIUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="PrivatniCasoviAPIFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="PrivatniCasoviAPIInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="553efc80-4cc1-45d4-92a2-505a7c9e23c8" ref="Microsoft.RedDog.Contract\ServiceContract\PrivatniCasoviFTNContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="667d151e-89e6-4eb7-a40d-47011aaf12ef" ref="Microsoft.RedDog.Contract\Interface\PrivatniCasoviAPI:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPI:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>