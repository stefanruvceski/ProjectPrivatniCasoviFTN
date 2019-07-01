<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="PrivatniCasoviFTN" generation="1" functional="0" release="0" Id="5b4902c7-81e1-4aa2-b50c-b09580185757" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="PrivatniCasoviFTNGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="PrivatniCasoviAPI:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/LB:PrivatniCasoviAPI:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="WorkerRole1:InputRequest" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/LB:WorkerRole1:InputRequest" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="PrivatniCasoviAPI:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapPrivatniCasoviAPI:DataConnectionString" />
          </maps>
        </aCS>
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
        <aCS name="WorkerRole1:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapWorkerRole1:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="WorkerRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapWorkerRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WorkerRole1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapWorkerRole1Instances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:PrivatniCasoviAPI:Endpoint1">
          <toPorts>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPI/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:WorkerRole1:InputRequest">
          <toPorts>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1/InputRequest" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapPrivatniCasoviAPI:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPI/DataConnectionString" />
          </setting>
        </map>
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
        <map name="MapWorkerRole1:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1/DataConnectionString" />
          </setting>
        </map>
        <map name="MapWorkerRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWorkerRole1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1Instances" />
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
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;PrivatniCasoviAPI&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;PrivatniCasoviAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot;&gt;&lt;e name=&quot;InputRequest&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
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
        <groupHascomponents>
          <role name="WorkerRole1" generation="1" functional="0" release="0" software="C:\Users\Stefan\Documents\GitHub\ProjectPrivatniCasoviFTN\PrivatniCasoviFTN\PrivatniCasoviFTN\csx\Debug\roles\WorkerRole1" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="InputRequest" protocol="tcp" portRanges="11000" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WorkerRole1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;PrivatniCasoviAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot;&gt;&lt;e name=&quot;InputRequest&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1Instances" />
            <sCSPolicyUpdateDomainMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1UpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="PrivatniCasoviAPIUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="WorkerRole1UpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="PrivatniCasoviAPIFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WorkerRole1FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="PrivatniCasoviAPIInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WorkerRole1Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="69bf81e2-2b90-4af6-b9c7-63aae5692381" ref="Microsoft.RedDog.Contract\ServiceContract\PrivatniCasoviFTNContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="b44a1831-c360-405a-9f6b-1e59f6d8634e" ref="Microsoft.RedDog.Contract\Interface\PrivatniCasoviAPI:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPI:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="595f0800-e8b9-4298-941e-a15b4ef7df72" ref="Microsoft.RedDog.Contract\Interface\WorkerRole1:InputRequest@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1:InputRequest" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>