﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 
namespace Tiled {
    using System.Xml.Serialization;

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://mapeditor.org", IsNullable=false)]
    public partial class properties {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("property")]
        public property[] property;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://mapeditor.org", IsNullable=false)]
    public partial class property {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mapeditor.org")]
    public partial class simpleDataT {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public encodingT encoding;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlTextAttribute(DataType="base64Binary")]
        public byte[] Value;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mapeditor.org")]
    public enum encodingT {
        
        /// <comentarios/>
        base64,
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mapeditor.org")]
    public partial class simpleImageT {
        
        /// <comentarios/>
        public simpleDataT data;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string id;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public formatT format;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool formatSpecified;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mapeditor.org")]
    public enum formatT {
        
        /// <comentarios/>
        png,
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://mapeditor.org", IsNullable=false)]
    public partial class map {
        
        /// <comentarios/>
        public properties properties;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("tileset")]
        public tileset[] tileset;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("layer", typeof(layer))]
        [System.Xml.Serialization.XmlElementAttribute("objectgroup", typeof(objectgroup))]
        public object[] Items;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public orientationT orientation;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public renderorderT renderorder;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool renderorderSpecified;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string width;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string height;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string tilewidth;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string tileheight;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://mapeditor.org", IsNullable=false)]
    public partial class tileset {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("image")]
        public tilesetImage[] image;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("tile")]
        public tilesetTile[] tile;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        public string firstgid;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string source;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string tilewidth;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string tileheight;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string spacing;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string margin;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    public partial class tilesetImage : simpleImageT {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute("format")]
        public formatT format1;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool format1Specified;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
        public string source;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string trans;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string width;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string height;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    public partial class tilesetTile {
        
        /// <comentarios/>
        public properties properties;
        
        /// <comentarios/>
        public simpleImageT image;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string id;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://mapeditor.org", IsNullable=false)]
    public partial class layer {
        
        /// <comentarios/>
        public properties properties;
        
        /// <comentarios/>
        public layerData data;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string width;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string height;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal opacity;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool opacitySpecified;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    public partial class layerData {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("tile")]
        public layerDataTile[] Items;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public encodingT encoding;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool encodingSpecified;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public compressionT compression;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool compressionSpecified;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    public partial class layerDataTile {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string gid;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mapeditor.org")]
    public enum compressionT {
        
        /// <comentarios/>
        gzip,
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://mapeditor.org", IsNullable=false)]
    public partial class objectgroup {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("object")]
        public @object[] @object;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string width;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string height;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://mapeditor.org")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://mapeditor.org", IsNullable=false)]
    public partial class @object {
        
        /// <comentarios/>
        public properties properties;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string x;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string y;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string width;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string height;
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mapeditor.org")]
    public enum orientationT {
        
        /// <comentarios/>
        orthogonal,
        
        /// <comentarios/>
        isometric,
        
        /// <comentarios/>
        hexagonal,
        
        /// <comentarios/>
        shifted,
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mapeditor.org")]
    public enum renderorderT {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("right-down")]
        rightdown,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("right-up")]
        rightup,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("left-down")]
        leftdown,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnumAttribute("left-up")]
        leftup,
    }
}
