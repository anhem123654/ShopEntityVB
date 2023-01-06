Imports AutoMapper
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Web.ManShop.Web.Models


Namespace ManShop.Web.Mappings
    Public Class AutoMapperConfiguration
        Shared Sub Configure()
            Mapper.Initialize(Sub(cfg)
                                  cfg.CreateMap(Of Post, PostViewModel)()
                                  cfg.CreateMap(Of PostCategory, PostCategoryViewModel)()
                                  cfg.CreateMap(Of Tag, TagViewModel)()
                                  cfg.CreateMap(Of ProductCategory, ProductCategoryViewModel)()
                                  cfg.CreateMap(Of Product, ProductViewModel)()
                                  cfg.CreateMap(Of ProductTag, ProductTagViewModel)()
                                  cfg.CreateMap(Of Footer, FooterViewModel)()
                                  cfg.CreateMap(Of Slide, SlideViewModel)()
                                  cfg.CreateMap(Of Page, PageViewModel)()
                                  cfg.CreateMap(Of ContactDetail, ContactDetailViewModel)()
                                  cfg.CreateMap(Of ApplicationGroup, ApplicationGroupViewModel)()
                                  cfg.CreateMap(Of ApplicationRole, ApplicationRoleViewModel)()
                                  cfg.CreateMap(Of ApplicationUser, ApplicationUserViewModel)()
                              End Sub)
        End Sub
    End Class
End Namespace
