# Sample Spring Boot application which uses log4j for logging.

In order to use any logging library other than Logback (default in Spring Boot), we need to exclude it from our dependencies.

```xml
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-web</artifactId>
    <exclusions>
        <exclusion>
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-starter-logging</artifactId>
        </exclusion>
    </exclusions>
</dependency>
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-log4j2</artifactId>
</dependency>
```

__Reference:__ [https://www.baeldung.com/spring-boot-logging](https://www.baeldung.com/spring-boot-logging)
