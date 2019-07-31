<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="PrivatniCasoviFTN" generation="1" functional="0" release="0" Id="dd65444e-7bcd-41d6-893e-6c5677e62382" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="PrivatniCasoviFTNGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="PrivatniCasoviAPI:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/LB:PrivatniCasoviAPI:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="WorkerRole1:PrivateClassInputRequest" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/LB:WorkerRole1:PrivateClassInputRequest" />
          </inToChannel>
        </inPort>
        <inPort name="WorkerRole1:SubjectInputRequest" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/LB:WorkerRole1:SubjectInputRequest" />
          </inToChannel>
        </inPort>
        <inPort name="WorkerRole1:UserInputRequest" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/LB:WorkerRole1:UserInputRequest" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="AttendedClassWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapAttendedClassWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AttendedClassWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapAttendedClassWorkerRoleInstances" />
          </maps>
        </aCS>
        <aCS name="ClassStatusWorkerRole:DataConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapClassStatusWorkerRole:DataConnectionString" />
          </maps>
        </aCS>
        <aCS name="ClassStatusWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapClassStatusWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ClassStatusWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/MapClassStatusWorkerRoleInstances" />
          </maps>
        </aCS>
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
        <lBChannel name="LB:WorkerRole1:PrivateClassInputRequest">
          <toPorts>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1/PrivateClassInputRequest" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:WorkerRole1:SubjectInputRequest">
          <toPorts>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1/SubjectInputRequest" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:WorkerRole1:UserInputRequest">
          <toPorts>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1/UserInputRequest" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapAttendedClassWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/AttendedClassWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAttendedClassWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/AttendedClassWorkerRoleInstances" />
          </setting>
        </map>
        <map name="MapClassStatusWorkerRole:DataConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/ClassStatusWorkerRole/DataConnectionString" />
          </setting>
        </map>
        <map name="MapClassStatusWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/ClassStatusWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapClassStatusWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/ClassStatusWorkerRoleInstances" />
          </setting>
        </map>
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
          <role name="AttendedClassWorkerRole" generation="1" functional="0" release="0" software="C:\Users\Stefan\Documents\GitHub\ProjectPrivatniCasoviFTN\PrivatniCasoviFTN\PrivatniCasoviFTN\csx\Debug\roles\AttendedClassWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AttendedClassWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AttendedClassWorkerRole&quot; /&gt;&lt;r name=&quot;ClassStatusWorkerRole&quot; /&gt;&lt;r name=&quot;PrivatniCasoviAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot;&gt;&lt;e name=&quot;PrivateClassInputRequest&quot; /&gt;&lt;e name=&quot;SubjectInputRequest&quot; /&gt;&lt;e name=&quot;UserInputRequest&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/AttendedClassWorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/AttendedClassWorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/AttendedClassWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="ClassStatusWorkerRole" generation="1" functional="0" release="0" software="C:\Users\Stefan\Documents\GitHub\ProjectPrivatniCasoviFTN\PrivatniCasoviFTN\PrivatniCasoviFTN\csx\Debug\roles\ClassStatusWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ClassStatusWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AttendedClassWorkerRole&quot; /&gt;&lt;r name=&quot;ClassStatusWorkerRole&quot; /&gt;&lt;r name=&quot;PrivatniCasoviAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot;&gt;&lt;e name=&quot;PrivateClassInputRequest&quot; /&gt;&lt;e name=&quot;SubjectInputRequest&quot; /&gt;&lt;e name=&quot;UserInputRequest&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/ClassStatusWorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/ClassStatusWorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/ClassStatusWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="PrivatniCasoviAPI" generation="1" functional="0" release="0" software="C:\Users\Stefan\Documents\GitHub\ProjectPrivatniCasoviFTN\PrivatniCasoviFTN\PrivatniCasoviFTN\csx\Debug\roles\PrivatniCasoviAPI" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;PrivatniCasoviAPI&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AttendedClassWorkerRole&quot; /&gt;&lt;r name=&quot;ClassStatusWorkerRole&quot; /&gt;&lt;r name=&quot;PrivatniCasoviAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot;&gt;&lt;e name=&quot;PrivateClassInputRequest&quot; /&gt;&lt;e name=&quot;SubjectInputRequest&quot; /&gt;&lt;e name=&quot;UserInputRequest&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
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
              <inPort name="PrivateClassInputRequest" protocol="tcp" portRanges="11001" />
              <inPort name="SubjectInputRequest" protocol="tcp" portRanges="11002" />
              <inPort name="UserInputRequest" protocol="tcp" portRanges="11000" />
            </componentports>
            <settings>
              <aCS name="DataConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WorkerRole1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AttendedClassWorkerRole&quot; /&gt;&lt;r name=&quot;ClassStatusWorkerRole&quot; /&gt;&lt;r name=&quot;PrivatniCasoviAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole1&quot;&gt;&lt;e name=&quot;PrivateClassInputRequest&quot; /&gt;&lt;e name=&quot;SubjectInputRequest&quot; /&gt;&lt;e name=&quot;UserInputRequest&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
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
        <sCSPolicyUpdateDomain name="ClassStatusWorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="AttendedClassWorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="AttendedClassWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="ClassStatusWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="PrivatniCasoviAPIFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WorkerRole1FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="AttendedClassWorkerRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="ClassStatusWorkerRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="PrivatniCasoviAPIInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WorkerRole1Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="142a33b2-6e50-4ce6-8cab-2db0eb1914f9" ref="Microsoft.RedDog.Contract\ServiceContract\PrivatniCasoviFTNContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="32fd98fc-210e-4684-91f0-4755bc3bb32c" ref="Microsoft.RedDog.Contract\Interface\PrivatniCasoviAPI:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/PrivatniCasoviAPI:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="6f6d137e-111e-4a20-85a9-9f4e3a8ab59f" ref="Microsoft.RedDog.Contract\Interface\WorkerRole1:PrivateClassInputRequest@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1:PrivateClassInputRequest" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="f08c65d4-34ed-492c-bc50-4231f9b82aac" ref="Microsoft.RedDog.Contract\Interface\WorkerRole1:SubjectInputRequest@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1:SubjectInputRequest" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="64481d27-38b9-486d-9121-2b6d906b76e8" ref="Microsoft.RedDog.Contract\Interface\WorkerRole1:UserInputRequest@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/PrivatniCasoviFTN/PrivatniCasoviFTNGroup/WorkerRole1:UserInputRequest" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>